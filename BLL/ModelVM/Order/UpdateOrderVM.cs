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
    }

}
