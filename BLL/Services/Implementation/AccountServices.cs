namespace BLL.Services.Implementation
{
    public class AccountServices(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager) : IAccountServices
    {
        #region Fields
        private readonly IMapper _mapper = mapper;
        private readonly UserManager<User> _userManager = userManager;
        private readonly SignInManager<User> _signInManager=signInManager;
        #endregion
        #region Implementation
        public async Task<(bool,string)> SignUp(SignUpvM signUpvM)
        {
            try
            {
                if (signUpvM.UploadImage is null)
                    signUpvM.Image = "avater.jpg";
                else
                    signUpvM.Image = UploadFiles.UploadFile("UserPersonnalImages", signUpvM.UploadImage);
                var identityUser = _mapper.Map<User>(signUpvM);
                identityUser.RoleName = "Customer";
                var result= await _userManager.CreateAsync(identityUser,signUpvM.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(identityUser, "Customer");
                }
                return (result.Succeeded,result.ToString());

            }
            catch 
            { 
                return (false,"something went Wrong");
            }
        }

        public async Task<bool> SignIn(string userName, string password)
        {
            try
            {
                var user = await GetUser(userName);
                if (user == null)
                    return false ;

                var result = await _signInManager.PasswordSignInAsync(userName, password, false, false);

                if (result.Succeeded)
                    return true;

                return false;
            }
            catch
            {
                return false;
            }

        }

        public async Task SignOut()
        {
           await _signInManager.SignOutAsync();
        }

        public async Task<User> GetUser(string userName)
        {
            try
            {
                var result =await _userManager.FindByNameAsync(userName);
                if (result == null)
                    return null;
                return result;
            }
            catch
            {
                return null;
            }
        }
        #endregion

    }
}
