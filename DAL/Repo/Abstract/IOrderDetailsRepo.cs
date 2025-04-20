namespace DAL.Repo.Abstract
{
    public interface IOrderDetailsRepo:IGenericRepo<OrderDetails>
    {
        Task<List<OrderDetails>> GetByOrderIdAsync(long orderId);
        void Delete(long id);
    }
}
