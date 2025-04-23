namespace BLL.Services.Abstract
{
    public interface IProfileServices
    {
        public  Task<ProfileVm> GetUserProfileAsync(string userId, string currentUserId);

    }
}
