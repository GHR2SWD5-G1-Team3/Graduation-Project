using System.Linq.Expressions;
using DAL.DataBase;

namespace DAL.Shared.Generic
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class, IDeletable,IEditable
    {
        private readonly ApplicationDBContext Db;
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

        public bool Delete(long Id)
        {
            try
            {
                var entity = Db.Set<T>().Find(Id);
                if (entity == null)
                    return false;
                var result = entity.Delete("Admin");
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

        public (bool, string?) Edit(long id ,string? user, Dictionary<string, object> updatedProperties)
        {
            try
            {
                var entity = Db.Set<T>().Find(id);
                if (entity == null)
                    return (false, "Invalid ID: Id not found");
                bool isEdited = entity.Edit(user , updatedProperties);
                if (isEdited)
                {
                    Db.SaveChanges();
                    return (true, "");
                }
                return (false, "Error updating Entity");
            }
            catch (Exception ex)
            {
                return (false, $"Error editing entity: {ex.Message}");
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
