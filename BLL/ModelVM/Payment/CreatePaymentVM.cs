namespace BLL.ModelVM.Payment
{
    public class CreatePaymentVM
    {
        public string Method { get; set; }
        public decimal AmountPaied { get; set; }
        public string State { get; set; }
        public long OrderId { get; set; }
    }
}
