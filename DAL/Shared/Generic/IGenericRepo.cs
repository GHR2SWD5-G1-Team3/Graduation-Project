namespace DAL.Shared.Generic
{
    public interface IGenericRepo<T> where T : class
    {
        (bool, string?) Create(T t);
        T Get(Expression<Func<T, bool>>? filter = null);
        List<T> GetAll(Expression<Func<T, bool>>? filter = null);
    }
}
