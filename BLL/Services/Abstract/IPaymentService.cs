namespace BLL.Services.Abstract
{
    public interface IPaymentService
    {
        Task<bool> Create(CreatePaymentVM payment);
        Task<PaymentVM> GetPaymentAsync(Expression<Func<Payment, bool>>? filter = null, params Expression<Func<Payment,object>>[] include);
        Task<List<PaymentVM>> GetAllPaymentsAsync(Expression<Func<Payment, bool>>? filter = null, params Expression<Func<Payment,object>>[] include);
        Task<string> CreateStripeSessionAsync(decimal amount, long orderId);
    }
}
