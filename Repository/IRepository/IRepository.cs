using Project1.Models;
using System.Linq.Expressions;

namespace Project1.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        // CRUD
        void CreateNew(T entity);
        void Edit(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll(string? includeProperty = null);
		IEnumerable<T> Get(Expression<Func<T, bool>> expression, string? includeProperty = null);
		IEnumerable<T> TestGet(Expression<Func<T, bool>> expression, Expression<Func<T, object>> includeProperty);

        public IEnumerable<T> TestGet2(Expression<Func<T, bool>> expression,
                                      params Expression<Func<T, object>>[] includeProperties);

		void Commit();
    }
}
