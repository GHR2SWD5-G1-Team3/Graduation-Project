
namespace BLL.Services.Implementation
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepo paymentRepo;
        private readonly IMapper mapper;

        public PaymentService(IPaymentRepo paymentRepo, IMapper mapper)
        {
            this.paymentRepo = paymentRepo;
            this.mapper = mapper;
        }

        public async Task<bool> Create(CreatePaymentVM payment)
        {
            try
            {
                var turnPayment = mapper.Map<Payment>(payment);
                var result = await paymentRepo.CreateAsync(turnPayment);
                if (result.Item1)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<List<PaymentVM>> GetAllPaymentsAsync(Expression<Func<Payment, bool>>? filter = null, params Expression<Func<Payment, object>>[] includes)
        {
            try
            {
                var payments = await paymentRepo.GetAllAsync(filter, includes);
                if (payments == null || payments.Count == 0)
                    return new List<PaymentVM>();

                var paymentVMs = mapper.Map<List<PaymentVM>>(payments);
                return paymentVMs;
            }
            catch (Exception)
            {
                return [];
            }
        }


        public async Task<PaymentVM> GetPaymentAsync(Expression<Func<Payment, bool>>? filter = null, params Expression<Func<Payment, object>>[] include)
        {
            try
            {
                var payment = await paymentRepo.GetAsync(filter, include);
                if (payment == null)
                    return null;

                var paymentVM = mapper.Map<PaymentVM>(payment);
                return paymentVM;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
