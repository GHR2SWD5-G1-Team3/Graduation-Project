namespace DAL.Repo.Abstract
{
    public interface ICategoryRepo : IGenericRepo<Category>
    {
        Task<(bool, string?)> Edit(string user, Category category, int Id);
        Task<bool> Delete(string user, int id);
    }

}

