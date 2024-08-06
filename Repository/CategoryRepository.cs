using Project1.Data;
using Project1.Models;
using Project1.Repository.IRepository;

namespace Project1.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        ApplicationDbContext context = new ApplicationDbContext();

        // CRUD
        public void CreateNew(Category category)
        {
            context.Add(category);
            context.SaveChanges();
        }

        public void Edit(Category category)
        {
            context.Update(category);
            context.SaveChanges();
        }

        public void Delete(Category category)
        {
            context.Remove(category);
            context.SaveChanges();
        }

        public IEnumerable<Category> GetAll()
        {
            return context.Categories.ToList();
        }

        public Category? GetOne(int id)
        {
            return context.Categories.Find(id);
        }
    }
}
