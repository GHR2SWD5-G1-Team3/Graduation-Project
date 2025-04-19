namespace BLL.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IEmailSender _emailSender;
        private readonly IUserServices _userService;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepo orderRepo, IEmailSender emailSender, IUserServices userService, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _emailSender = emailSender;
            _userService = userService;
            _mapper = mapper;
        }

        // Create a new order
        public async Task<bool> CreateOrderAsync(CreateOrderVM model, string userId)
        {
            try
            {
                // Create a new Order instance, passing userId to the constructor
                var order = new Order(
                    model.TotalPrice,
                    model.IsPaid,
                    model.IsDelivered,
                    model.PhoneNumber,
                    model.City,
                    model.Street,
                    model.PaymentMethod,
                    userId  // Pass the userId to the constructor
                );

                // Add the order to the repository
                var result = await _orderRepo.AddAsync(order);
                if (result)
                {
                    // Send confirmation email after order creation
                    await SendOrderConfirmationEmailAsync(userId, order.Id);
                    return true;  // Return true on success
                }

                return false;  // Return false if creation failed
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Update an existing order
        public async Task<bool> UpdateOrderAsync(long id, UpdateOrderVM updateOrderVM)
        {
            try
            {
                var order = await _orderRepo.GetByIdAsync(id);
                if (order == null)
                {
                    return false;
                }

                // Update the order using the Edit method (assuming this method exists in your Order class)
                order.Edit(updateOrderVM.ModifiedBy, updateOrderVM.TotalPrice, updateOrderVM.PhoneNumber, updateOrderVM.City, updateOrderVM.Street, updateOrderVM.PaymentMethod);
                var updateResult = await _orderRepo.SaveAsync(); // Assuming SaveAsync persists changes to the database

                return updateResult;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Delete an existing order
        public async Task<bool> DeleteOrderAsync(long id, string deletedBy)
        {
            try
            {
                var order = await _orderRepo.GetByIdAsync(id);
                if (order == null)
                {
                    return false;
                }

                var result = order.Delete(deletedBy);
                if (result)
                {
                    await _orderRepo.SaveAsync(); // Persist the deletion
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Retrieve order by Id
        public async Task<Order> GetOrderByIdAsync(long id)
        {
            try
            {
                return await _orderRepo.GetByIdAsync(id); // Returning an Order object instead of DisplayOrderVM
            }
            catch (Exception)
            {
                return null;
            }
        }

        // Retrieve all orders with pagination
        public async Task<PaginatedList<Order>> GetAllOrdersAsync(int pageNumber, int pageSize, string userId = null)
        {
            try
            {
                var orders = userId == null
                    ? await _orderRepo.GetAllAsync()
                    : await _orderRepo.GetAllByUserIdAsync(userId);

                return PaginatedList<Order>.Create(orders, pageNumber, pageSize); // Adjusted return type
            }
            catch (Exception)
            {
                return null;
            }
        }

        // Send order confirmation email
        public async Task SendOrderConfirmationEmailAsync(string userEmail, long orderId)
        {
            var order = await _orderRepo.GetByIdAsync(orderId);
            if (order == null) return;

            var subject = "Order Confirmation";
            var body = $"Your order with ID {orderId} has been placed successfully. The total price is {order.TotalPrice:C}.";
            await _emailSender.SendEmailAsync(userEmail, subject, body);
        }

        // Update the order status (Pending, Delivered, Cancelled)
        public async Task<bool> UpdateOrderStatusAsync(long orderId, OrderStatus newStatus)
        {
            var order = await _orderRepo.GetByIdAsync(orderId);
            if (order == null)
            {
                return false;
            }

            var isUpdated = order.UpdateStatus(newStatus);
            if (isUpdated)
            {
                await _orderRepo.SaveAsync(); // Assuming you have a method to commit changes in the repository
            }

            return isUpdated;
        }
    }
}
