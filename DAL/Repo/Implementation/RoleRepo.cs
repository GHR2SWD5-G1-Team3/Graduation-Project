

namespace DAL.Repo.Implementation
{
    public class RoleRepo(ApplicationDBContext context) : GenericRepo<Role>(context), IRoleRepo
    {
    }
}
