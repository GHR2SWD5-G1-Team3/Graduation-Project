namespace DAL.Repo.Abstract
{
    public interface ICategoryRepo : IGenericRepo<Category>
    {
        Task<(bool, string?)> Edit( Category category, int Id);
        Task<bool> Delete( int id);
    }

}

