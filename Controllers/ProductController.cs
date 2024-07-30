using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.Data;
using Project1.Models;

namespace Project1.Controllers
{
    public class ProductController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public IActionResult Mobiles()
        {
            var result = context.Products.Include(e=>e.Category).Where(e => e.CategoryId == 1).ToList();

            //Response.Cookies.Append("name", "Mohamed");
            //TempData["tempname"] = "Mohamed";

            return View(result);
        }

        // /Product/Details/1 ==> route value
        // /Product/Details?productId=1 ==> Query String
        public IActionResult Details(int productId)
        {
            var result = context.Products.Find(productId);
            //var result = context.Products.Where()
            return View(result); 
        }

        public IActionResult Create()
        {
            var result = context.Categories.ToList();
            ViewData["listOfCategories"] = result;

            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();

            return RedirectToAction("Create");
        }

        public IActionResult Edit(int id)
        {
            var result = context.Products.Find(id);

            //string name = "Mohamed";
            //int x = 10;
            //List<double> doubles = new List<double>() { 10, 20, 30 };

            //ViewData["name"] = name;
            //ViewData["value"] = x;
            //ViewData["list"] = doubles;

            //ViewBag.name = name;
            //ViewBag.value = x;
            //ViewBag.list = doubles;

            ViewData["listOfCategories"] = context.Categories.ToList();

            return result != null ? View(result) : RedirectToAction("NotFound");
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            context.Products.Update(product);
            context.SaveChanges();

            return RedirectToAction("Mobiles");
        }

        public IActionResult Delete(int id)
        {
            var result = context.Products.Find(id);

            if (result != null)
            {
                context.Products.Remove(result);
                context.SaveChanges();
                return RedirectToAction("Mobiles");
            }
            else
            {
                return RedirectToAction("NotFound", "Home");
            }
        }


    }
}
