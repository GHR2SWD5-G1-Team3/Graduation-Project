using static System.Net.Mime.MediaTypeNames;

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
        public bool IsDeleted { get; set; } = false;
        public string? DeletedBy { get; private set; }
        public DateTime DeletedOn { get; set; }
        public string? ModifiedBy { get; private set; }
        public DateTime ModifiedOn { get; private set; }

        public bool Delete(string? User)
        {
            if (User == null) return false;

            IsDeleted = !IsDeleted;
            DeletedBy = User;
            DeletedOn = DateTime.Now;
            return true;
        }
        public bool Edit(string? user, string code, DateTime? expiredAt, int? usageLimit, int discount)
        {
            if (user == null) return false;
            Code = code;
            ExpiredAt = expiredAt;
            UsageLimit = usageLimit;
            Discount = discount;

            ModifiedBy = user;
            ModifiedOn = DateTime.Now;
            return true;
        }

    }
}
