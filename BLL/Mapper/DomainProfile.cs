namespace BLL.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile() 
        {
            //Order
            CreateMap<Order, CreateOrderVM>();
            CreateMap<Order, DisplayOrderVM>();


        }
    }
}
