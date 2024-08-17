using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Project1.Data;
using Project1.Models;
using Project1.Repository.IRepository;
using System.Linq.Expressions;

namespace Project1.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext context;
        private DbSet<T> dbSet;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void CreateNew(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Edit(T entity)
        {
            dbSet.Update(entity);
        }

		public IEnumerable<T> Get(Expression<Func<T, bool>> expression, string? includeProperty = null)
		{
			if (includeProperty != null)
				return dbSet.Include(includeProperty).Where(expression);
			else
				return dbSet.Where(expression);
		}

		public IEnumerable<T> GetAll(string? includeProperty = null)
        {
            if (includeProperty != null)
                return dbSet.Include(includeProperty);
            else
                return dbSet;
        }

		public IEnumerable<T> TestGet(
            Expression<Func<T, bool>> expression, Expression<Func<T, object>> includeProperty)
		{
			return dbSet.Include(includeProperty).Where(expression);
		}

		public IEnumerable<T> TestGet2(Expression<Func<T, bool>> expression, 
                                      params Expression<Func<T, object>>[] includeProperties)
		{
			IQueryable<T> query = dbSet;
			foreach (var includeProperty in includeProperties)
			{
				query = query.Include(includeProperty);
			}
			query = query.Where(expression);

			return query;
		}
	}
}
