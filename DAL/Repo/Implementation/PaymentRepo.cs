
namespace DAL.Repo.Implementation
{
    public class PaymentRepo : GenericRepo<Payment>, IPaymentRepo
    {
        public PaymentRepo(ApplicationDBContext context) : base(context)
        {
        }
    }
}
