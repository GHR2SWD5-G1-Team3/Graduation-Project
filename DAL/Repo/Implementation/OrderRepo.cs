namespace DAL.Repo.Implementation
{
    public class OrderRepo : GenericRepo<Order>, IOrderRepo
    {
        public OrderRepo(ApplicationDBContext context) : base(context)
        { }
        

    }
}
