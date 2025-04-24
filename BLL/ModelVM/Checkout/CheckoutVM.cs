using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ModelVM.Checkout
{
    public class CheckoutVM
    {
        public List<DisplayCartDetailsVM> CartItems { get; set; }
        public decimal TotalPrice { get; set; }
        // Add properties for the form (First Name, Last Name, etc.)
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        // Add additional properties for the form fields as required
    }

}
