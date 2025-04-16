namespace DAL.Repo.Abstract
{
    public interface IReviewRepo
    {
        (bool, string?) Create(Review review);
        List<Review> GetAll(Expression<Func<Review, bool>>? filter = null);
        Review Get(Expression<Func<Review, bool>>? filter = null);
        bool Update(Review review);
        bool Delete(Review review);
    }
}
