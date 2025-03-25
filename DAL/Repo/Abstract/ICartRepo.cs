namespace DAL.Repo.Abstract
{
    public interface ICartRepo :IGenericRepo<Cart>
    {
        (bool, string?) Edit(string user, Cart cart , long Id);
        bool DeleteById(string user, long id);
    }
}
