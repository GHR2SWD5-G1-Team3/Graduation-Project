namespace DAL.Repo.Implementation
{
    public class UserRepo(ApplicationDBContext context) : GenericRepo<User>(context), IUserRepo
    {
    }
}
