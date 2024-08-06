using Project1.Models;

namespace Project1.Repository.IRepository
{
    public interface ICategoryRepository
    {
        // CRUD
        void CreateNew(Category category);
        void Edit(Category category);
        void Delete(Category category);
        IEnumerable<Category> GetAll();
        Category? GetOne(int id);
    }
}
