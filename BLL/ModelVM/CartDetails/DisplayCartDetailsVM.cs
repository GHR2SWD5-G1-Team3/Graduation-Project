﻿namespace BLL.ModelVM.CartDetails
{
    public class DisplayCartDetailsVM
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string? Name { get; set; } 
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal TotalPrice => Price * Quantity; 
        public string? ImagePath { get; set; }
       
    }
}
