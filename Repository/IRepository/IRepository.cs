using Project1.Models;

namespace Project1.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        // CRUD
        void CreateNew(T entity);
        void Edit(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
        T? GetOne(int id);

        void Commit();
    }
}
