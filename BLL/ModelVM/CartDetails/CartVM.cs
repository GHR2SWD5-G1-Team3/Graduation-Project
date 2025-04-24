using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ModelVM.CartDetails
{
    public class CartVM
    {
        public List<DisplayCartDetailsVM> CartItems { get; set; } = new();
        public decimal Subtotal => CartItems.Sum(i => i.TotalPrice);
        public decimal Shipping => 3.00m; // Flat shipping rate
        public decimal Total => Subtotal + Shipping;
    }
}
