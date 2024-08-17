using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project1.Models;
using Project1.Repository.IRepository;
using Stripe.Checkout;

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
            TempData["shoppingCart"] = JsonConvert.SerializeObject(result);
            ViewBag.Total = result.Sum(e => e.Count * e.Product.Price);
            return View(result);
        }

        public IActionResult Increment(int cartId)
        {
            var result = shoppingCartRepository.Get(e => e.Id == cartId).FirstOrDefault();
            result.Count += 1;
            shoppingCartRepository.Commit();

            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Decrement(int cartId)
        {
            var result = shoppingCartRepository.Get(e => e.Id == cartId).FirstOrDefault();
            if (result.Count == 1)
                shoppingCartRepository.Delete(result);
            else
                result.Count -= 1;

            shoppingCartRepository.Commit();

            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Delete(int cartId)
        {
            var result = shoppingCartRepository.Get(e => e.Id == cartId).FirstOrDefault();
            shoppingCartRepository.Delete(result);

            shoppingCartRepository.Commit();

            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Pay()
        {
            var items = JsonConvert.DeserializeObject<IEnumerable<ShoppingCart>>((string)TempData["shoppingCart"]);

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = $"{Request.Scheme}://{Request.Host}/checkout/success",
                CancelUrl = $"{Request.Scheme}://{Request.Host}/checkout/cancel",
            };
            foreach (var model in items)
            {
                var result = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = model.Product.Name,
                        },
                        UnitAmount = (long)model.Product.Price * 100,
                    },
                    Quantity = model.Count,
                };
                options.LineItems.Add(result);
            }

            var service = new SessionService();
            var session = service.Create(options);
            return Redirect(session.Url);
        }
    }
}
