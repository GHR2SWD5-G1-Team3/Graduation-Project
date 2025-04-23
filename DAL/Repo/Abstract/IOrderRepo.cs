namespace DAL.Repo.Abstract
{
    public interface IOrderRepo :IGenericRepo<Order>
    {
        Task<List<Order>> GetAllByUserIdAsync(string userId);
        Task<List<Order>> GetAllAsync();
        Task<bool> AddAsync(Order order);
        Task<Order> GetByIdAsync(long orderId);
        Task<Order?> GetOrderWithDetailsAsync(long orderId);
        Task<List<Order>> GetOrdersByUserAsync(string userId);
        Task<bool> BulkCreateAsync(Order order, List<OrderDetails> details);
        (bool, string?) Edit(string user, Order order, long orderId);
        Task<bool> SoftDeleteAsync(string user, long orderId);
        Task<bool> UpdateStatusAsync(long orderId, OrderStatus newStatus);
        Task ClearCartAsync(string userId);
        Task SaveAsync();
        Task UpdateOrderAsync(Order order);
    }
}
