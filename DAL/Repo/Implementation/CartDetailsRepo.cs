
namespace DAL.Repo.Implementation
{
    public class CartDetailsRepo : GenericRepo<CartDetails>, ICartDetailsRepo
    {
        private readonly ApplicationDBContext _context;

        public CartDetailsRepo(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }
        public void Delete(long id) 
        {
            if (id == 0)
            {
                Console.WriteLine("Invaild ID");
                return;
            }
            var targetItem = _context.CartDetails.Find(id);
            if (targetItem == null) 
            {
                Console.WriteLine("Entity not found");
                return;

            }
            _context.CartDetails.Remove(targetItem);
            _context.SaveChanges();
        }

        public async Task UpdateAsync(CartDetails cartDetails)
        {
            _context.CartDetails.Update(cartDetails);
            await _context.SaveChangesAsync();
        }
    }
}
