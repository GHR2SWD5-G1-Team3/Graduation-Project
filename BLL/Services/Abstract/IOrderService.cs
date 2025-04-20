namespace BLL.Services.Abstract
{
    public interface IOrderService
    {
        Task<bool> UpdateOrderStatusAsync(long orderId, OrderStatus newStatus);
        Task<Order> GetOrderByIdAsync(long orderId);
        Task<PaginatedList<Order>> GetAllOrdersAsync(int pageNumber, int pageSize, string userId = null);
        Task<bool> CreateOrderAsync(CreateOrderVM model, string userId);
        Task SendOrderConfirmationEmailAsync(string userEmail, long orderId); // Ensure this method is in the interface

    }


}
