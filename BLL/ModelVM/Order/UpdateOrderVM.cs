using BLL.ModelVM.Order.BLL.ModelVM.Order;
using System.IO;

namespace BLL.ModelVM.Order
{
    public class UpdateOrderVM
    {
        public decimal TotalPrice { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PaymentMethod { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsPaid { get; set; }
        public bool IsDelivered { get; set; }
        public List<OrderProductVM> Products { get; set; } = new();
        public List<DisplayCartDetailsVM> CartItems { get; set; } = new();
        public decimal Subtotal => CartItems?.Sum(ci => ci.TotalPrice) ?? 0;
        public string Status { get; set; } // Order status (Pending, Delivered, Cancelled)
    }

}
