namespace BLL.Services.Abstract
{
    public interface IOrderServices
    {
        //Command
        (bool, string?) Create(CreateOrderVM createOrderVM);
        (bool, string?) Update(long id, UpdateOrderVM updateOrderVM);
        (bool, string?) Delete(long id, string deletedBy);


        //Query
        DisplayOrderVM Get(Expression<Func<Order, bool>>? filter = null);
        List<DisplayOrderVM> GetAll(Expression<Func<Order, bool>>? filter = null);

    }
}
