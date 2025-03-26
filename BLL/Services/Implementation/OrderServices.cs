namespace BLL.Services.Implementation
{
    public class OrderServices(IOrderRepo orderRepo, IMapper mapper, ApplicationDBContext dbContext) : IOrderServices
    {
        private readonly IOrderRepo orderRepo = orderRepo;
        private readonly IMapper mapper = mapper;
        private readonly ApplicationDBContext dbContext=dbContext;

        public (bool, string?) Create(CreateOrderVM createOrderVM)
        {
            try
            {
                var order = mapper.Map<Order>(createOrderVM);
                return orderRepo.Create(order);
            }
            catch (Exception ex) 
            {
                return (false, ex.Message);
            }

        }

        public DisplayOrderVM Get(Expression<Func<Order, bool>>? filter = null)
        {
            try
            {
                var orderSource = orderRepo.Get(filter);
                var order = mapper.Map<DisplayOrderVM>(orderSource);
                return order;
            }
            catch
            {
                return null;
            }
        }

        public List<DisplayOrderVM> GetAll(Expression<Func<Order, bool>>? filter = null)
        {
            try
            {
                var ordersSource = orderRepo.GetAll(filter);
                List < DisplayOrderVM > listOfOrder = new List<DisplayOrderVM>();
                foreach (var order in ordersSource)
                {
                    var item = mapper.Map<DisplayOrderVM>(order);
                    listOfOrder.Add(item);
                }
                return listOfOrder;
            }
            catch
            {
                return null;
            }
        }

        public (bool, string?) Update(long id, UpdateOrderVM updateOrderVM)
        {
            try
            {
                var order = dbContext.Orders.FirstOrDefault(o => o.Id == id);
                if (order == null)
                {
                    return (false, "Can't Find The Desired Order !");

                }
                else
                {
                    order.Edit(updateOrderVM.ModifiedBy, updateOrderVM.TotalPrice, updateOrderVM.PhoneNumber, updateOrderVM.City, updateOrderVM.Street, updateOrderVM.PaymentMethod);
                    dbContext.SaveChanges();
                    return (true, "Updated Sucessfully !");
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
