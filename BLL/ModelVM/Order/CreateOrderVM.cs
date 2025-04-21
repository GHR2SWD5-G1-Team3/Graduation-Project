using BLL.ModelVM.Order.BLL.ModelVM.Order;
using System.IO;

namespace BLL.ModelVM.Order
{
    public class CreateOrderVM
    {
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        // List of OrderProducts in the final order
        public List<OrderProductVM> Products { get; set; } = new();

        // List of CartItems before the order is finalized
        public List<DisplayCartDetailsVM> CartItems { get; set; } = new();

        // Optional: Computed total price from CartItems
        public decimal Subtotal => CartItems?.Sum(ci => ci.TotalPrice) ?? 0;

        public string Address { get; set; }
    }

}
