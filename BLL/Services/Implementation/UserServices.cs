namespace BLL.Services.Implementation
{
    public class UserServices(IMapper mapper, UserManager<User> userManager,IUserRepo userRepo,ICartService service) : IUserServices
    {
        #region Fields
        private readonly IMapper _mapper = mapper;
        private readonly UserManager<User> _userManager = userManager;
        private readonly IUserRepo _userRepo = userRepo;
        private readonly ICartService cartService=service;
        #endregion
        #region Implementation
        public async Task<(bool,string)> CreateAsync(AddNewUserVM newUser)
        {
            try
            {
                if (newUser.UploadImage is null)
                    newUser.Image = "avater.jpg";
                else
                    newUser.Image = UploadFiles.UploadFile("UserPersonnalImages", newUser.UploadImage);
                var identityUser = _mapper.Map<User>(newUser);
                var result = await _userManager.CreateAsync(identityUser, newUser.Password);
                if (result.Succeeded) 
                {
                    await _userManager.AddToRoleAsync(identityUser,newUser.RoleName);
                    await cartService.AddCart(identityUser.Id);
                }
                return (result.Succeeded,result.ToString());

            }
            catch
            {
                return (false,"something went wrong");
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
                List<DisplayUser> displayUsers = [];
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
                string imagePath = "avatar.jpg";
                if (user.Image is not null && editUser.UploadImage is not null && user.Image != "avatar.jpg")
                {
                    var deleteImage = UploadFiles.RemoveFile("UserPersonnalImages", user.Image);
                    if (deleteImage == "File Not Deleted")
                    {
                        return false;
                    }
                    imagePath = UploadFiles.UploadFile("UserPersonnalImages", editUser.UploadImage);
                }
                var result =await _userRepo.UpdateAsync(editUser.EditBy,editUser.FirstName,editUser.LastName,imagePath,editUser.Phone,editUser.City,editUser.Government,editUser.Id);
                return result;
            }
            catch
            {
                return false;
            }
        }
        public async Task<IdentityUser> GetUserByIdAsync(string userId)
        {
            try
            {
                var user = await _userRepo.GetAsync(a => a.Id == userId);
                return user;
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}
