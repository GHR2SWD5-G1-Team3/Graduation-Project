namespace DAL.Repo.Implementation
{
    public class ReviewRepo : IReviewRepo
    {
        private readonly ApplicationDBContext _context;

        public ReviewRepo(ApplicationDBContext context)
        {
            _context = context;
        }

        public (bool, string?) Create(Review review)
        {
            _context.Reviews.Add(review);
            var result = _context.SaveChanges() > 0;
            return (result, result ? null : "Error saving review");
        }

        public List<Review> GetAll(Expression<Func<Review, bool>>? filter = null)
        {
            return filter == null
                ? _context.Reviews.Where(r => !r.IsDeleted).ToList()
                : _context.Reviews.Where(filter).ToList();
        }

        public Review Get(Expression<Func<Review, bool>>? filter = null)
        {
            return _context.Reviews.FirstOrDefault(filter);
        }

        public bool Update(Review review)
        {
            _context.Reviews.Update(review);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(Review review)
        {
            _context.Reviews.Remove(review);
            return _context.SaveChanges() > 0;
        }
    }
}
