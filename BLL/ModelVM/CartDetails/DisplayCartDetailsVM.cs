﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ModelVM.CartDetails
{
    public class DisplayCartDetailsVM
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; } 
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Price * Quantity; 
        public string ImageUrl { get; set; } 
    }
}
