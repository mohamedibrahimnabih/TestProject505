using Microsoft.AspNetCore.Mvc;

namespace Project1.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult success()
        {
            return View();
        }
        public IActionResult cancel()
        {
            return View();
        }
    }
}
