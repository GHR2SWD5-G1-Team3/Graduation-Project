using DAL.Repo.Implementation;
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
        //private readonly ILogger _logger;


        public OrderService(
            IOrderRepo orderRepo,
            IOrderDetailsRepo orderDetailsRepo,
            ICartRepo cartRepo,
            IMapper mapper,
            IEmailSender emailSender,
            UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor,
            ApplicationDBContext context
            //, ILogger logger
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
            //_logger = logger;

        }

        //public async Task<bool> CreateOrderAsync(CreateOrderVM model, string userId)
        //{
        //    try
        //    {
        //        var user = await _userManager.FindByIdAsync(userId);
        //        if (user == null) return false;

        //        decimal totalPrice = model.Products.Sum(p => p.UnitPrice * p.Quantity);

        //        var order = new Order(
        //            totalPrice,
        //            isPaied: false,
        //            isDelivered: false,
        //            model.PhoneNumber,
        //            model.Street,
        //            model.City,
        //            model.PaymentMethod,
        //            userId
        //        );

        //        var orderDetails = model.Products.Select(p => new OrderDetails(
        //            productId: p.Id,
        //            orderId: order.Id,
        //            price: p.UnitPrice,
        //            quantity: p.Quantity
        //        )).ToList();

        //        foreach (var detail in orderDetails)
        //        {
        //            order.AddOrderDetail(detail);
        //        }

        //        foreach (var detail in orderDetails)
        //        {
        //            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id ==1);
        //            if (product == null || product.Quantity < detail.Quantity)
        //                return false;

        //            product.ReduceQuantity(detail.Quantity);
        //        }

        //        await _context.SaveChangesAsync(); // Ensure inventory updates are saved

        //        var success = await _orderRepo.BulkCreateAsync(order, orderDetails);
        //        if (!success) return false;

        //        await _orderRepo.ClearCartAsync(userId);

        //        await SendOrderConfirmationEmailAsync(user.Email, order.Id);

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        // TODO: Log the exception (e.g., using ILogger)
        //        Console.WriteLine($"[OrderService] Error while creating order: {ex.Message}");
        //        return false;
        //    }
        //}
        public async Task<bool> CreateOrderAsync(CreateOrderVM model, string userId)
        {
            try
            {
               var order =new Order(model.Subtotal,false,false,model.PhoneNumber,model.City,model.Street,model.PaymentMethod,userId);
               var orderResult = await _orderRepo.CreateAsync(order);
                if (!orderResult.Item1)
                    return false;
                foreach (var product in model.Products) 
                {
                    var orderDetails = new OrderDetails(product.Id, order.Id, product.Total, product.Quantity);
                    var result= await _orderDetailsRepo.CreateAsync(orderDetails);
                    if (!result.Item1)
                    {
                        return false;
                    }
                }
                await _orderRepo.ClearCartAsync(userId);
                return true;
            }
            catch 
            {
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
                //_logger.LogError(ex, "An error occurred while creating an order.");
                throw new  Exception("An error occurred while creating an order.");
            }
        }

        public async Task<Order?> GetOrderWithDetailsAsync(long id)
        {
            return await _orderRepo.GetOrderWithDetailsAsync(id);
        }

     

        public async Task<List<Order>> GetAllOrdersAsync(int pageNumber, int pageSize, string? userId = null)
        {
            var allOrders = string.IsNullOrEmpty(userId)
                ? await _orderRepo.GetAllAsync()
                : await _orderRepo.GetAllByUserIdAsync(userId);

            return allOrders;
        }

        public Task<bool> UpdateOrderStatusAsync(long orderId, OrderStatus status, string updatedBy)
        {
            throw new NotImplementedException();
        }
        public async Task<Order> GetOrderAsync(Expression<Func<Order,bool>>? filter =null)
        {
            return await _orderRepo.GetAsync(filter);
        }
        public async Task UpdateAsync(long orderId, string user)
        {
            var order = await _orderRepo.GetByIdAsync(orderId);
            if (order != null)
            {
                 order.Edit(user, order.Subtotal, order.PhoneNumber, order.City, order.Street, order.PaymentMethod);
                 await _orderRepo.UpdateOrderAsync(order);
            }
        }

    }
}
