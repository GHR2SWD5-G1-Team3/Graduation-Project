namespace BLL.Services.Abstract
{
    public interface IAccountServices
    {
      public Task<bool> SignUp(SignUpvM signUpvM);
      public Task<bool> SignIn(string userName, string password);
      public Task SignOut();
      public Task <User> GetUser(string userName);

    }
}
