namespace BLL.Services.Implementation
{
    public class UserServices(IMapper mapper, UserManager<User> userManager,IUserRepo userRepo, IAccountServices accountServices) : IUserServices
    {
        #region Fields
        private readonly IMapper _mapper = mapper;
        private readonly UserManager<User> _userManager = userManager;
        private readonly IUserRepo _userRepo = userRepo;
        #endregion
        #region Implementation
        public async Task<bool> CreateAsync(AddNewUser newUser)
        {
            try
            {
                var identityUser = _mapper.Map<User>(newUser);
                var result = await _userManager.CreateAsync(identityUser, newUser.Password);
                return result.Succeeded;

            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string userId, string deletedBy)
        {
            try
            {
               var result= await _userRepo.DeleteAsync(deletedBy ,userId);
                return result;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<DisplayUser>> GetAllAsync(Expression<Func<User, bool>>? filter = null, params Expression<Func<User, object>>[] includeProperty)
        {
            try
            {
                List<DisplayUser> displayUsers = new List<DisplayUser>();
                var users = await _userRepo.GetAllAsync(filter, includeProperty);
                foreach (var user in users)
                {
                    displayUsers.Add(_mapper.Map<DisplayUser>(user));
                }
                    return displayUsers;
            }
            catch
            {
                return null;
            }
        }

        public async Task<DisplayUser> GetAsync(Expression<Func<User, bool>>? filter = null)
        {
            try
            {
                var user= await _userRepo.GetAsync(filter);
                var displayUser=_mapper.Map<DisplayUser>(user);
                return displayUser;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(EditUser editUser)
        {
            try
            {
                var user = await _userRepo.GetAsync(a=>a.Id==editUser.Id);
                if(user.Image is not null)
                {
                    var deleteImage = UploadFiles.RemoveFile("UserPersonnalImages", user.Image);
                    if (deleteImage == "File Not Deleted")
                    {
                        return false;
                    }
                }
                string imagePath = null;
                if (editUser.Image is not null)
                {
                     imagePath = UploadFiles.UploadFile("UserPersonnalImages", editUser.Image);
                }
                var result =await _userRepo.UpdateAsync(editUser.EditBy,editUser.FirstName,editUser.LastName,imagePath,editUser.Phone,editUser.Address,editUser.Id);
                return result;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
