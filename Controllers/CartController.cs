using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project1.Models;
using Project1.Repository.IRepository;

namespace Project1.Controllers
{
    public class CartController : Controller
    {
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public CartController(IShoppingCartRepository shoppingCartRepository, UserManager<ApplicationUser> userManager)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.userManager = userManager;
        }
        
        public IActionResult Index(int id)
        {
            var userId = userManager.GetUserId(User);
            if(id!=0)
            {
                // Add To Cart
                ShoppingCart shoppingCart = new()
                {
                    ProductId = id,
                    Count = 1,
                    ApplicationUserId = userId
                };
                shoppingCartRepository.CreateNew(shoppingCart);
                shoppingCartRepository.Commit();

                TempData["success"] = "Add product successfuly";
                return RedirectToAction("Index", "Home");
            }

            // Retrieve Data From DB
            var result = shoppingCartRepository.Get(e => e.ApplicationUserId == userId, nameof(Product));
            return View(result);
        }
    }
}
