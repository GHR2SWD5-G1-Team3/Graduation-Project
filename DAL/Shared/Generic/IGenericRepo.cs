namespace DAL.Shared.Generic
{
    public interface IGenericRepo<T> where T : class
    {
        Task<(bool, string?)> CreateAsync(T t);
        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null);
        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includeProperties);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includeProperties);
        (bool, string?) Create(T t);
        T Get(Expression<Func<T, bool>>? filter = null);
        List<T> GetAll(Expression<Func<T, bool>>? filter = null);
    }
}
