using Microsoft.EntityFrameworkCore;

namespace DAL.Repo.Implementation
{
    public class CartRepo : GenericRepo<Cart>, ICartRepo
    {
        public CartRepo(ApplicationDBContext context) : base(context)
        {
        }
        
        public async Task AddCartItemAsync(CartDetails item)
        {
            await Db.CartDetails.AddAsync(item);
            await Db.SaveChangesAsync();
        }

        
        public async Task<CartDetails?> GetCartItemAsync(long cartId, long productId)
        {
            return await Db.CartDetails
            .FirstOrDefaultAsync(cd => cd.CartId == cartId && cd.ProductId == productId);
        }
        public async Task UpdateCartItemAsync(CartDetails item)
        {
            var existingItem = await Db.CartDetails
            .FirstOrDefaultAsync(cd => cd.CartId == item.CartId && cd.ProductId == item.ProductId);

            if (existingItem != null)
            {
                existingItem.Edit(item.Quantity, item.Price, item.Name);
                await Db.SaveChangesAsync();
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
