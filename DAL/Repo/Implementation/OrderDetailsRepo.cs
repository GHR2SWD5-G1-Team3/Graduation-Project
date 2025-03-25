
namespace DAL.Repo.Implementation
{
    public class OrderDetailsRepo : GenericRepo<OrderDetails>, IOrderDetailsRepo
    {
        public OrderDetailsRepo(ApplicationDBContext context) : base(context)
        {
        }
    }
}
