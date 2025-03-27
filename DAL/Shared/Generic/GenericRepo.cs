namespace DAL.Shared.Generic
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        protected readonly ApplicationDBContext Db;
        public GenericRepo(ApplicationDBContext context)
        {
            Db = context;
        }

        public (bool,string?) Create(T t)
        {
            try
            {
                Db.Add(t);
                Db.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, $"An error occurred: {ex.Message}");
            }
        }

        public T? Get(Expression<Func<T, bool>>? filter = null)
        {
            if (filter == null)
                return Db.Set<T>().FirstOrDefault();
            else 
                return Db.Set<T>().FirstOrDefault(filter);
        }

        public List<T> GetAll(Expression<Func<T, bool>>? filter = null)
        {
            if (filter == null)
                return Db.Set<T>().ToList();
            var result = Db.Set<T>().Where(filter).ToList();
            return result;
        }
    }
}
