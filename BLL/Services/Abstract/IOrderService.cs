public interface IOrderService
{
    Task<bool> CreateOrderAsync(CreateOrderVM model, string userId);
    Task<Order?> GetOrderWithDetailsAsync(long id);
    Task<bool> UpdateOrderStatusAsync(long orderId, OrderStatus status, string updatedBy);
    Task<List<Order>> GetAllOrdersAsync(int pageNumber, int pageSize, string? userId = null);
    Task SendOrderConfirmationEmailAsync(string userEmail, long orderId);

}