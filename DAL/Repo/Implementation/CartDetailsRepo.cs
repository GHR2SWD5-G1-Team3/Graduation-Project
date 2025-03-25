
namespace DAL.Repo.Implementation
{
    public class CartDetailsRepo : GenericRepo<CartDetails>, ICartDetailsRepo
    {
        public CartDetailsRepo(ApplicationDBContext context) : base(context)
        {
        }
    }
}
