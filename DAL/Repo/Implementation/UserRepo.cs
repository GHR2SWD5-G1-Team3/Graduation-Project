namespace DAL.Repo.Implementation
{
    public class UserRepo(ApplicationDBContext context) : GenericRepo<User>(context), IUserRepo
    {
     
        public async Task<bool> DeleteAsync(string? deletedBy, string userId)
        {
            try
            {
                var deleteUser=await Db.Users.FirstOrDefaultAsync(a=>a.Id == userId);
                if (deleteUser != null) 
                {
                    deleteUser.Delete(deletedBy);
                    await Db.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(string? editBy, string fName, string lName, string imagepath , string phone, string address, string userId)
        {
            try
            {
                var editUser = await Db.Users.FirstOrDefaultAsync(a => a.Id == userId);
                if (editUser != null)
                {
                    editUser.Edit(editBy,fName,lName,imagepath,address,phone);
                    await Db.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
