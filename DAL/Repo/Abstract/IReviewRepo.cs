using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAL.Repositories
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
