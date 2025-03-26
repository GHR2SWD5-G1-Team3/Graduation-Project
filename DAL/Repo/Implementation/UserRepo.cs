namespace DAL.Repo.Implementation
{
    public class UserRepo : GenericRepo<User>, IUserRepo
    {
        public UserRepo(ApplicationDBContext context) : base(context)
        {
        }
    }
}
