using Microsoft.AspNetCore.Mvc;
using Project1.Models;
using Project1.Repository.IRepository;
using System.Diagnostics;

namespace Project1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepositroy productRepositroy;

        public HomeController(ILogger<HomeController> logger, IProductRepositroy productRepositroy)
        {
            _logger = logger;
            this.productRepositroy = productRepositroy;
        }

        public IActionResult Index()
        {
            var result = productRepositroy.GetAll(nameof(Category));
            return View(result);
        }

        public IActionResult NotFound()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
