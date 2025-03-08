namespace DAL.Enities
{
    public class Coupon(string code, DateTime? expiredAt, int? usageLimit, int discount)
    {
        public long Id { get; private set; }
        public string Code { get; private set; } = code;
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public DateTime? ExpiredAt { get; private set; } = expiredAt;
        public int? UsageLimit { get; private set; } = usageLimit;
        public int Discount { get;private set; } = discount;
        public int UsedNumber { get; private set; } = 0;
    }
}
