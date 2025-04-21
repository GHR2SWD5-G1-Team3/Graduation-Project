namespace BLL.Services.Implementation
{
    public class RoleServices(RoleManager<Role> roleManager, IRoleRepo roleRepo, IMapper mapper) : IRoleServices
    {
        #region Fields
        private readonly RoleManager<Role> _roleManager = roleManager;
        private readonly IRoleRepo _roleRepo = roleRepo;
        private readonly IMapper _mapper = mapper;
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
        #endregion

    }
}
