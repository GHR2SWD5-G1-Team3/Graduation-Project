namespace DAL.Entities
{
    public class Payment(string method, decimal amountPaied, string state, long orderId)
    {
        public long Id { get; private set; }
        public string Method { get; private set; } = method;
        public decimal AmountPaied { get; private set; } = amountPaied;
        public string State { get; private set; } = state;
        [ForeignKey(nameof(Order))]
        public long OrderId { get; private set; } = orderId;
        public Order? Order { get; set; }

    }
}
