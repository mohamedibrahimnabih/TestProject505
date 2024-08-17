using Project1.Data;
using Project1.Models;
using Project1.Repository.IRepository;

namespace Project1.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ApplicationDbContext context;

        public ShoppingCartRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
