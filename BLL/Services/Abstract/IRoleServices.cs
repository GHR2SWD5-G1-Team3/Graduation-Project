using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BLL.Services.Abstract
{
    public interface IRoleServices
    {
        //Command
        Task<(bool,string)> AddRole(string roleName);
        Task<(bool,string)> SoftDeleted(string roleId, string deletedBy);
        Task<(bool,string)> RemoveRole(string roleId);
        Task<(bool,string)> UpdatUserRole(string userId, string roleName);
        //Query
        Task<(List<DisplayRoleVm>,string)> GetAllRoles(Expression<Func<Role, bool>>? filter = null);
        
    }
}
