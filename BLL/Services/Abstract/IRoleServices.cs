namespace BLL.Services.Abstract
{
    public interface IRoleServices
    {
        //Command
        Task<(bool,string)> AddRole(string roleName);
        Task<(bool,string)> RemoveRole(string roleName);
        //Query
        Task<(List<DisplayRoleVm>,string)> GetAllRoles(Expression<Func<Role, bool>>? filter = null);
        
    }
}
