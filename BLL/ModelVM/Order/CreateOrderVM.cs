
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

        public List<DisplayCartDetailsVM> CartItems { get; set; } = [];
        public decimal Subtotal { set; get; }

        public string FullAddress => $"{Street}, {City}";
    }
}
