namespace DAL.Repo.Abstract
{
    public interface IReviewRepo : IGenericRepo<Review>
    {
        (bool, string?) Edit(string user, Review review, long Id);
        bool DeleteById(string user, long id);
    
    }
}
