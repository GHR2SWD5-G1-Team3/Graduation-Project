
namespace BLL.ModelVM.Order
{
    public class CreateOrderVM
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string PaymentMethod { get; set; }
        public string Street { get; set; }  

        public List<OrderProductVM> Products { get; set; } = new();
        public List<DisplayCartDetailsVM> CartItems { get; set; } = new List<DisplayCartDetailsVM>();
        public decimal Subtotal => CartItems?.Sum(item => item.TotalPrice) ?? 0;

        public string FullAddress => $"{Street}, {City}";
    }
}
