namespace DAL.Enities
{
    public class Payment(string method, decimal amountPaied, string state)
    {
        public double Id { get; private set; }
        public string Method { get; private set; } = method;
        public decimal AmountPaied { get; private set; } = amountPaied;
        public string State { get; private set; } = state;
        [ForeignKey(nameof(Order))]
        public long OrderId { get; set; }
        public Order? Order { get; set; }

    }
}
