using Microsoft.AspNetCore.Mvc;
using Project1.Data;
using Project1.Models;

namespace Project1.Controllers
{
    public class CategoryController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public IActionResult Index()
        {
            //ViewData["name"] = Request.Cookies["name"];
            //ViewData["name"] = TempData["tempname"];

            //ViewData["state"] = TempData["state"];

            var result = context.Categories.ToList();
            return View(result);
        }

        //[HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        //public IActionResult SaveNew(string Name, string Description)
        [HttpPost]
        public IActionResult Create(Category category)
        {
            //Category category = new Category()
            //{
            //    Name = Name,
            //    Description = Description
            //};

            context.Categories.Add(category);
            context.SaveChanges();
            TempData["state"] = "Add Category successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var result = context.Categories.Find(id);
            return result != null ? View(result) : RedirectToAction("NotFound", "Home");
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            context.Categories.Update(category);
            context.SaveChanges();
            TempData["state"] = "Update Category successfully";

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var result = context.Categories.Find(id);

            if(result != null)
            {
                context.Categories.Remove(result);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("NotFound", "Home");
            }
        }
    }
}
