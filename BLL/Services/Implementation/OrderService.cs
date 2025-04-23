using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BLL.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IOrderDetailsRepo _orderDetailsRepo;
        private readonly ICartRepo _cartRepo;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDBContext _context;
        private readonly ILogger<OrderService> _logger;


        public OrderService(
            IOrderRepo orderRepo,
            IOrderDetailsRepo orderDetailsRepo,
            ICartRepo cartRepo,
            IMapper mapper,
            IEmailSender emailSender,
            UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor,
            ApplicationDBContext context
            , ILogger<OrderService> logger
)
        {
            _orderRepo = orderRepo;
            _orderDetailsRepo = orderDetailsRepo;
            _cartRepo = cartRepo;
            _mapper = mapper;
            _emailSender = emailSender;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _logger = logger;

        }

        public async Task<bool> CreateOrderAsync(CreateOrderVM model, string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null) return false;

                decimal totalPrice = model.Products.Sum(p => p.UnitPrice * p.Quantity);

                var order = new Order(
                    totalPrice,
                    isPaied: false,
                    isDelivered: false,
                    model.PhoneNumber,
                    model.City,
                    model.Street,
                    model.PaymentMethod,
                    userId
                );

                var orderDetails = model.Products.Select(p => new OrderDetails(
                    productId: p.Id,
                    orderId: order.Id,
                    price: p.UnitPrice,
                    quantity: p.Quantity
                )).ToList();

                foreach (var detail in orderDetails)
                {
                    order.AddOrderDetail(detail);
                }

                foreach (var detail in orderDetails)
                {
                    var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == detail.ProductId);
                    if (product == null || product.Quantity < detail.Quantity)
                        return false;

                    product.ReduceQuantity(detail.Quantity);
                }

                await _context.SaveChangesAsync(); // Ensure inventory updates are saved

                var success = await _orderRepo.BulkCreateAsync(order, orderDetails);
                if (!success) return false;

                await _orderRepo.ClearCartAsync(userId);

                await SendOrderConfirmationEmailAsync(user.Email, order.Id);

                return true;
            }
            catch (Exception ex)
            {
                // TODO: Log the exception (e.g., using ILogger)
                Console.WriteLine($"[OrderService] Error while creating order: {ex.Message}");
                return false;
            }
        }

        public async Task SendOrderConfirmationEmailAsync(string userEmail, long orderId)
        {
            try
            {
                var order = await _orderRepo.GetOrderWithDetailsAsync(orderId);
                if (order == null) return;

                var subject = $"Order #{order.Id} Confirmation";
                var message = $"Thank you for your order. Your order status is: {order.Status}.<br/>" +
                              $"Total items: {order.OrderDetails?.Count}<br/>" +
                              $"Total price: {order.OrderDetails?.Sum(od => od.Quantity * od.Price)}";

                await _emailSender.SendEmailAsync(userEmail, subject, message);
            }
            catch (Exception ex)
            {
                // TODO: Log the exception (e.g., using ILogger)
                _logger.LogError(ex, "An error occurred while creating an order.");

            }
        }

        public async Task<Order?> GetOrderWithDetailsAsync(long id)
        {
            return await _orderRepo.GetOrderWithDetailsAsync(id);
        }

        public async Task<bool> UpdateOrderStatusAsync(long orderId, OrderStatus newStatus)
        {
            return await _orderRepo.UpdateStatusAsync(orderId, newStatus);
        }

        public async Task<PaginatedList<Order>> GetAllOrdersAsync(int pageNumber, int pageSize, string? userId = null)
        {
            var allOrders = string.IsNullOrEmpty(userId)
                ? await _orderRepo.GetAllAsync()
                : await _orderRepo.GetAllByUserIdAsync(userId);

            return PaginatedList<Order>.Create(allOrders.AsQueryable(), pageNumber, pageSize);
        }
    }
}
