﻿
namespace DAL.Repo.Abstract
{
    public interface ICartDetailsRepo:IGenericRepo<CartDetails>
    {
        void Delete(long id);
        Task UpdateAsync(CartDetails cartDetails);
    }
}
