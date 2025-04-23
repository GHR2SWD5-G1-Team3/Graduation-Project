using BLL.ModelVM.Checkout;

namespace BLL.Services.Abstract
{
    public interface IOrderService
    {
        Task<bool> UpdateOrderStatusAsync(long orderId, OrderStatus newStatus);
        Task<PaginatedList<Order>> GetAllOrdersAsync(int pageNumber, int pageSize, string? userId = null);
        Task<bool> CreateOrderAsync(CreateOrderVM model, string userId);
        Task SendOrderConfirmationEmailAsync(string userEmail, long orderId); // Ensure this method is in the interface
        Task<Order?> GetOrderWithDetailsAsync(long id);
        Task<Order> GetOrderAsync(Expression<Func<Order, bool>>? filter = null);
        Task UpdateAsync(long orderId, string user);
    }


}
