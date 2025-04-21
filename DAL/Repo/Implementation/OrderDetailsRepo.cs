
namespace DAL.Repo.Implementation
{
    public class OrderDetailsRepo : GenericRepo<OrderDetails>, IOrderDetailsRepo
    {
        private readonly ApplicationDBContext _context;

        public OrderDetailsRepo(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<OrderDetails>> GetByOrderIdAsync(long orderId)
        {
            return await _context.OrderDetails
                .Include(od => od.Product)
                .Where(od => od.OrderId == orderId)
                .ToListAsync();
        }

        public void Delete(long id)
        {
            var target = _context.OrderDetails.Find(id);
            if (target == null)
            {
                Console.WriteLine("OrderDetails entity not found.");
                return;
            }

            _context.OrderDetails.Remove(target);
            _context.SaveChanges();
        }

        public Task AddAsync(OrderDetails orderDetail)
        {
            throw new NotImplementedException();
        }
    }

}
