using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ModelVM.Order
{
    namespace BLL.ModelVM.Order
    {
        public class OrderDetailsVM
        {
            public int OrderId { get; set; }
            public string PhoneNumber { get; set; }
            public string City { get; set; }
            public string Street { get; set; }
            public string PaymentMethod { get; set; } 
            public bool IsPaid { get; set; }
            public bool IsDelivered { get; set; } 

            public List<OrderProductVM> Products { get; set; }
            public decimal TotalPrice => Products?.Sum(p => p.Total) ?? 0;
        }
    }

}
