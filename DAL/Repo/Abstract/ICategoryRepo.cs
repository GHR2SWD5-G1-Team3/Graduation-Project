namespace DAL.Repo.Abstract
{
    public interface ICategoryRepo : IGenericRepo<Category>
    {
        (bool, string?) Edit(string user,Category category , int Id);
        bool Delete(string user, int id);
    }

}

