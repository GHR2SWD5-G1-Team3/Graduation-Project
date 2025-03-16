using DAL.DataBase;
using DAL.Repo.Abstract;
using DAL.Shared.Generic;

namespace DAL.Repo.Implementation
{
    public class UserRepo : GenericRepo<User>, IUserRepo
    {
        public UserRepo(ApplicationDBContext context) : base(context)
        {
        }
    }
}
