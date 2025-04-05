namespace DAL.Repo.Implementation
{
    public class ReviewRepo(ApplicationDBContext context) : GenericRepo<Review>(context), IReviewRepo
    {
        public (bool, string?) Edit(string user, Review review, long Id)
        {
            try
            {
                var Review = Db.Reviews.Where(r => r.Id == Id).FirstOrDefault();
                if (Review == null)
                    return (false, "review Not Found Database");
                var result = Review.Edit(user,Review.Comment,Review.Rate ,Review.UserId,Review.ProductId);
                if (result)
                {
                    Db.SaveChanges();
                    return (true, null);
                }
                return (false, "Please Enter User Modifier");

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public bool DeleteById(string user, long Id)
        {
            try
            {
                var review = Db.Reviews.FirstOrDefault(cat => cat.Id == Id);
                if (review == null)
                    return false;
                var result = review.Delete(user);
                if (result)
                {
                    Db.SaveChanges();
                    return (true);
                }
                return (false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting entity: {ex.Message}");
                return false;
            }
        }

    }
}
