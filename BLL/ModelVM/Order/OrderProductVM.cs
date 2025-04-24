namespace BLL.ModelVM.Order
{
    public class OrderProductVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public float DiscountPercentage { get; set; }
        public string Imagepath { get; set; }
        public decimal Total => UnitPrice * Quantity;
    }
}
