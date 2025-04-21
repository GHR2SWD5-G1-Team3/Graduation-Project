
using DAL.DataBase;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services.Concrete
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

        public OrderService(
            IOrderRepo orderRepo,
            IOrderDetailsRepo orderDetailsRepo,
            ICartRepo cartRepo,
            IMapper mapper,
            IEmailSender emailSender,
            UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor,
            ApplicationDBContext context)
        {
            _orderRepo = orderRepo;
            _orderDetailsRepo = orderDetailsRepo;
            _cartRepo = cartRepo;
            _mapper = mapper;
            _emailSender = emailSender;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _context = context;

        }

        public async Task<bool> CreateOrderAsync(CreateOrderVM model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            // Calculate total price based on the products in the model
            decimal totalPrice = model.Products.Sum(p => p.UnitPrice * p.Quantity);

            // Create the order entity
            var order = new Order(
                totalPrice,
                isPaied: false,  // Default: Order is not paid when created
                isDelivered: false, // Default: Order is not delivered
                model.PhoneNumber,
                model.City,
                model.Street,
                model.PaymentMethod,
                userId
            );

            // Create OrderDetails and associate them with the order
            var orderDetails = model.Products.Select(p => new OrderDetails(
                productId: p.ProductId,
                orderId: order.Id, // Link the details to the order
                price: p.UnitPrice,
                quantity: p.Quantity
            )).ToList();

            // Add OrderDetails to the Order
            foreach (var detail in orderDetails)
            {
                order.AddOrderDetail(detail);
            }

            // Reduce product quantity based on the order
            foreach (var detail in orderDetails)
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == detail.ProductId);
                if (product == null || product.Quantity < detail.Quantity)
                    return false;  // Insufficient stock or product not found

                product.ReduceQuantity(detail.Quantity); 
            }

            // Save order and order details using BulkCreateAsync
            var success = await _orderRepo.BulkCreateAsync(order, orderDetails);
            if (!success) return false;

            // Clear the cart after successful order placement
            await _orderRepo.ClearCartAsync(userId);

            // Send an order confirmation email to the user
            await SendOrderConfirmationEmailAsync(user.Email, order.Id);

            return true;
        }


        public async Task SendOrderConfirmationEmailAsync(string userEmail, long orderId)
        {
            var order = await _orderRepo.GetOrderWithDetailsAsync(orderId);
            if (order == null) return;

            var subject = $"Order #{order.Id} Confirmation";
            var message = $"Thank you for your order. Your order status is: {order.Status}.<br/>" +
                          $"Total items: {order.OrderDetails?.Count}<br/>" +
                          $"Total price: {order.OrderDetails?.Sum(od => od.Quantity * od.Price)}";

            await _emailSender.SendEmailAsync(userEmail, subject, message);
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
