namespace BLL.ModelVM.Payment
{
    public class PaymentVM
    {
        public long Id { get; set; }
        public string Method { get; set; }
        public decimal AmountPaied { get; set; }
        public string State { get; set; }
        public string UserId { get; set; }
        public DateTime PaymentDate { get; set; }
        public long OrderId { get; set; }
        public decimal OrderTotalPrice { get; set; }
    }
}
