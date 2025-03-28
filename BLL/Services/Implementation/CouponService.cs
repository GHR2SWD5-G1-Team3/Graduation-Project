using DAL.Shared.Generic;

namespace BLL.Services.Implementation
{
    public class CouponService : ICouponService
    {
        private readonly IGenericRepo<Coupon> repo;
        private readonly MapperConfiguration mapperConfig ;
        private readonly IMapper mapper;
        public CouponService(IGenericRepo<Coupon> Repo , MapperConfiguration configuration)
        {
            repo = Repo;
            mapperConfig = configuration;
            mapper = mapperConfig.CreateMapper();
        }
        public bool Create(string Code, DateTime? ExpiredAt, int? UsageLimit, int Discount)
        {
            try
            { 
                var Annoymus = new {Code , ExpiredAt, UsageLimit, Discount};
                var coupon = mapper.Map<Coupon>(Annoymus);
                repo.Create(coupon);
                return true;
            }
            catch 
            {
                throw new Exception("Error");
            }
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

       

        public List<Coupon> GetAll(Expression<Func<Coupon, bool>>? filter = null)
        {
            try
            {
                if (filter is null)
                    return repo.GetAll();
                return repo.GetAll(filter);
            }
            catch (Exception e)
            {
                throw new Exception($"An Error Happened :{e}");
            }
            
        }

        public Coupon GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(string code, DateTime? expiredAt, int? usageLimit, int discount)
        {
            throw new NotImplementedException();
        }
    }
}
