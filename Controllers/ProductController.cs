using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project1.Data;
using Project1.Models;
using Project1.Repository.IRepository;

namespace Project1.Controllers
{
    public class ProductController : Controller
    {
		private readonly IProductRepositroy productRepositroy;
		private readonly ICategoryRepository categoryRepository;
		private readonly ApplicationDbContext context;

		public ProductController(IProductRepositroy productRepositroy, ICategoryRepository categoryRepository, ApplicationDbContext context)
        {
			this.productRepositroy = productRepositroy;
			this.categoryRepository = categoryRepository;
			this.context = context;
		}

        public IActionResult Mobiles()
        {
			//var result = context.Products.Include(e=>e.Category).Where(e => e.CategoryId == 1).ToList();

			//var result = productRepositroy.Get(e => e.CategoryId == 1, includeProperty: nameof(Category));
			//var result = productRepositroy.TestGet(e => e.CategoryId == 1, e=>e.Category);

            var result = productRepositroy.TestGet2(
                e => e.CategoryId == 1,
                e => e.Category, e => e.Category, e => e.Category);

            //Response.Cookies.Append("name", "Mohamed");
            //TempData["tempname"] = "Mohamed";

            return View(result);
        }

        // /Product/Details/1 ==> route value
        // /Product/Details?productId=1 ==> Query String
        public IActionResult Details(int productId)
        {
            var result = productRepositroy.Get(e=>e.Id == productId).FirstOrDefault();
            //var result = context.Products.Where()
            return View(result); 
        }

        public IActionResult Create()
        {
            var result = categoryRepository.GetAll().Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Name
            }).ToList();

            ViewData["listOfCategories"] = result;


            Product product = new();
            return View(product);
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if(ModelState.IsValid)
            {
                productRepositroy.CreateNew(product);
                productRepositroy.Commit();


				return RedirectToAction("Create");
            }

            var result = categoryRepository.GetAll();
            ViewData["listOfCategories"] = result;
            return View(product);
        }

        public IActionResult Edit(int id)
        {
            var result = productRepositroy.Get(e => e.Id == id).FirstOrDefault();

            //string name = "Mohamed";
            //int x = 10;
            //List<double> doubles = new List<double>() { 10, 20, 30 };

            //ViewData["name"] = name;
            //ViewData["value"] = x;
            //ViewData["list"] = doubles;

            //ViewBag.name = name;
            //ViewBag.value = x;
            //ViewBag.list = doubles;

            ViewData["listOfCategories"] = categoryRepository.GetAll().Select(e=>new SelectListItem(e.Name, e.Id.ToString()));

            return result != null ? View(result) : RedirectToAction("NotFound");
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
			productRepositroy.Edit(product);
			productRepositroy.Commit();

			return RedirectToAction("Mobiles");
        }

        public IActionResult Delete(int id)
        {
            var result = productRepositroy.Get(e => e.Id == id).FirstOrDefault();

			if (result != null)
            {
				productRepositroy.Delete(result);
				productRepositroy.Commit();
				return RedirectToAction("Mobiles");
            }
            else
            {
                return RedirectToAction("NotFound", "Home");
            }
        }


    }
}
