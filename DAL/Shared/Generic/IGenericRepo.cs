namespace DAL.Shared.Generic
{
    public interface IGenericRepo<T> where T : class
    {
        (bool, string?) Create(T t);
        (bool, string?) Edit(long id, string? user, Dictionary<string, object> updatedProperties);
        bool Delete(long Id);
        T Get(Expression<Func<T, bool>>? filter = null);
        List<T> GetAll(Expression<Func<T, bool>>? filter = null);
    }
}
