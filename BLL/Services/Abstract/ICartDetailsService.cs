using BLL.ModelVM.CartDetails;
using DAL.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Abstract
{
    public interface ICartDetailsService
    {
        (bool, string) AddToCart(CartDetails cartDetails);
        void RemoveFromCart(int cartDetailId);
        List<DisplayCartDetailsVM> GetAllCartDetails(Expression<Func<CartDetails, bool>>? filter = null);
        DisplayCartDetailsVM GetCartDetails(Expression<Func<CartDetails, bool>>? filter = null);
    }

}
