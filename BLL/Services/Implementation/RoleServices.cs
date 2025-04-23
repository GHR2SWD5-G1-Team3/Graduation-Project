namespace BLL.Services.Implementation
{
    public class RoleServices(RoleManager<Role> roleManager, IRoleRepo roleRepo, IMapper mapper ,UserManager<User> userManager) : IRoleServices
    {
        #region Fields
        private readonly RoleManager<Role> _roleManager = roleManager;
        private readonly IRoleRepo _roleRepo = roleRepo;
        private readonly IMapper _mapper = mapper;
        private readonly UserManager<User> _userManager = userManager;
        #endregion
        #region Implementation
        public async Task<(bool, string)> AddRole(string roleName)
        {
            try
            {
              var identityRole=new Role(roleName);
              var result=await _roleManager.CreateAsync(identityRole);
              return(result.Succeeded,result.ToString());
                
            }
            catch(Exception ex) 
            {
                return (false, ex.Message);
            }
        }


        public async Task<(bool, string)> RemoveRole(string roleId)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(roleId);
                if(role == null)
                {
                    return (false, "Role Doesn't Exist!");
                }
                var result = await _roleManager.DeleteAsync(role);
                return(result.Succeeded,result.ToString());

            }
            catch(Exception ex) 
            { 
                return(false,ex.Message);
            }
        }
        public async Task<(List<DisplayRoleVm>?,string)> GetAllRoles(Expression<Func<Role, bool>>? filter = null)
        {
            try
            {
                List<DisplayRoleVm> displayRoleVms = [];
                var rolesList = await _roleRepo.GetAllAsync(filter);
                if (rolesList == null)
                    return (null, "No Role inserted Yet!");
                foreach (var role in rolesList)
                {
                    displayRoleVms.Add(_mapper.Map<DisplayRoleVm>(role));
                }
                return (displayRoleVms, "Success!");
            }
            catch (Exception ex)
            {
                return(null, ex.Message);
            }
        }

        public async Task<(bool, string)> SoftDeleted(string roleId, string deletedBy)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(roleId);
                if (role == null)
                {
                    return (false, "Role Doesn't Exist!");
                }
                role.DeletedBy = deletedBy;
                role.IsDeleted = true;
                var result = await _roleManager.UpdateAsync(role);
                return (result.Succeeded, result.ToString());
            }
            catch (Exception ex) 
            { 
                return (false, ex.Message);   
            }
        }

        public async Task<(bool, string)> UpdatUserRole(string userId, string roleName)
        {

            try
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role == null || role.IsDeleted == true)
                {
                    return (false, "Role Doesn't Exist!");
                }
                var user=await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return (false, "User Not Found");
                }
                await _userManager.RemoveFromRoleAsync(user, user.RoleName);
                await _userManager.AddToRoleAsync(user, roleName);
                user.RoleName = roleName;
                var result= await _userManager.UpdateAsync(user);
                return (result.Succeeded, result.ToString());
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        #endregion

    }
}
