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
            public long Id { get; set; }
     
            public string PhoneNumber { get; set; }
            public string City { get; set; }
            public string Street { get; set; }
            public string PaymentMethod { get; set; } 
            public bool IsPaid { get; set; }
            public bool IsDelivered { get; set; } 
            public OrderStatus Status { get; set; } // Order status (Pending, Delivered, Cancelled)
            public DateTime CreatedDate { get; set; } // Date when the order was created
            public string UserId { get; set; } // The ID of the user who made the order


            public List<OrderProductVM> Products { get; set; }
            public decimal TotalPrice => Products?.Sum(p => p.Total) ?? 0;
        }
    }

}
