﻿namespace DAL.Repo.Abstract
{
    public interface IUserRepo :IGenericRepo<User>
    {
        Task<bool> UpdateAsync(string? user, string fName, string lName, string imagepath, string phone, string address, string userId);
        Task<bool> DeleteAsync(string? User,string userId);
    }
}
