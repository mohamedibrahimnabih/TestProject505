using Microsoft.AspNetCore.Mvc;

namespace Project1.Controllers
{
    public class Account : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
