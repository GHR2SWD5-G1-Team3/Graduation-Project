﻿namespace DAL.Shared.Generic
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        protected readonly ApplicationDBContext Db;
        public GenericRepo(ApplicationDBContext context)
        {
            Db = context;
        }

        public async Task<(bool,string?)> CreateAsync(T t)
        {
            try
            {
                Db.Add(t);
                await Db.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, $"An error occurred: {ex.Message}");
            }
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null)
        {
            if (filter == null)
                return await Db.Set<T>().FirstOrDefaultAsync();
            else 
                return await Db.Set<T>().FirstOrDefaultAsync(filter);
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includeProperties)
        {
            // remeber Db.Set return an IQuerable (deferred Excution)
            IQueryable<T> query = Db.Set<T>();

            // Apply includes
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    // chain the querey
                    query = query.Include(includeProperty);
                }
            }

            // Apply filter
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();

        }

        public (bool, string?) Create(T t)
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

        public T Get(Expression<Func<T, bool>>? filter = null)
        {
            if (filter == null)
                return Db.Set<T>().FirstOrDefault();
            else
                return Db.Set<T>().FirstOrDefault(filter);
        }

        public List<T> GetAll(Expression<Func<T, bool>>? filter = null)
        {
            // remeber Db.Set return an IQuerable (deferred Excution)
            IQueryable<T> query = Db.Set<T>();

            // Apply filter
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();
        }
    }
}
