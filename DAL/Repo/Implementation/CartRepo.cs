namespace DAL.Repo.Implementation
{
    public class CartRepo : GenericRepo<Cart>, ICartRepo
    {
        public CartRepo(ApplicationDBContext context) : base(context)
        {
        }
        public (bool, string?) Edit(string user, Cart cart , long Id)
        {
            try
            {
                var Cart = Db.Carts.Where(c => c.Id == Id).FirstOrDefault();
                if (Cart == null)
                    return (false, "Cart Not Found in Database");
                var result = Cart.Edit(user,Cart.UserId );
                if (result)
                {
                    Db.SaveChanges();
                    return (true, null);
                }
                return (false, "Please Enter User Modifier");

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public async Task<bool> DeleteById(string user, long Id)
        {
            try
            {
                var cart = await Db.Carts.FirstOrDefaultAsync(c => c.Id == Id);
                if (cart == null)
                    return false;
                var result = cart.Delete(user);
                if (result)
                {
                   await Db.SaveChangesAsync();
                    return (true);
                }
                return (false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting entity: {ex.Message}");
                return false;
            }
        }
       
    }
}
