namespace BLL.Services.Abstract
{
    public interface IUserServices
    {
        Task<(bool,string)> CreateAsync(AddNewUserVM newUser);
        Task<bool> UpdateAsync(EditUser editUser);
        Task<List<DisplayUser>> GetAllAsync(Expression<Func<User, bool>>? filter = null, params Expression<Func<User, object>>[] includeProperty);
        Task<DisplayUser> GetAsync(Expression<Func<User, bool>>? filter = null);
        Task<bool> DeleteAsync(string userId, string deletedBy);
        Task<IdentityUser> GetUserByIdAsync(string userId);

    }
}
