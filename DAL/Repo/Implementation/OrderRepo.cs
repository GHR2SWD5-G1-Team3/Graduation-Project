using Microsoft.EntityFrameworkCore;

namespace DAL.Repo.Implementation
{
    public class OrderRepo : GenericRepo<Order>, IOrderRepo
    {
        private readonly ApplicationDBContext _context;

        public OrderRepo(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Order?> GetOrderWithDetailsAsync(long orderId)
        {
            return await _context.Orders
                .Include(o => o.Details)
                .ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId && !o.IsDeleted);
        }

        public async Task<List<Order>> GetOrdersByUserAsync(string userId)
        {
            return await _context.Orders
                .Include(o => o.Details)
                .ThenInclude(d => d.Product)
                .Where(o => o.UserId == userId && !o.IsDeleted)
                .ToListAsync();
        }

        public async Task<bool> BulkCreateAsync(Order order, List<OrderDetails> details)
        {
            if (order == null || details == null || details.Count == 0)
                return false;

            // Attach details to order entity
            foreach (var detail in details)
            {
                order.AddOrderDetail(detail); // Assuming you use a method like this in your entity
            }

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public (bool, string?) Edit(string user, Order updatedOrder, long orderId)
        {
            var existingOrder = _context.Orders.FirstOrDefault(o => o.Id == orderId);
            if (existingOrder == null)
                return (false, "Order not found.");

            var result = existingOrder.Edit(
                user,
                updatedOrder.TotalPrice,
                updatedOrder.PhoneNumber,
                updatedOrder.City,
                updatedOrder.Street,
                updatedOrder.PaymentMethod
            );

            if (!result)
                return (false, "Invalid user for modification.");

            _context.SaveChanges();
            return (true, null);
        }

        public async Task<bool> SoftDeleteAsync(string user, long orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return false;

            var result = order.Delete(user);
            if (!result) return false;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateStatusAsync(long orderId, OrderStatus newStatus)
        {
            try
            {
                var order = await Db.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
                if (order == null)
                    return false;

                var updated = order.UpdateStatus(newStatus);
                if (updated)
                    await Db.SaveChangesAsync();

                return updated;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating order status: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> AddAsync(Order order)
        {
            _context.Orders.Add(order);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Order> GetByIdAsync(long orderId)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<List<Order>> GetAllByUserIdAsync(string userId)
        {
            return await _context.Orders.Where(o => o.UserId == userId).ToListAsync();
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }

      
        public async Task<bool> SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync(); // Persist changes to the database
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        Task<bool> IOrderRepo.SaveAsync()
        {
            throw new NotImplementedException();
        }


        //public async Task<bool> UpdateStatusAsync(long orderId, OrderStatus newStatus)
        //{
        //    var order = await _context.Orders.FindAsync(orderId);
        //    if (order != null)
        //    {
        //        order.Status = newStatus;
        //        await _context.SaveChangesAsync();
        //        return true;
        //    }
        //    return false;

        //}
    }
}
