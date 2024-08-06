using Microsoft.AspNetCore.Mvc;
using Project1.Data;
using Project1.Models;
using Project1.Repository;
using Project1.Repository.IRepository;

namespace Project1.Controllers
{
    public class CategoryController : Controller
    {

        ICategoryRepository Repository;// = new CategoryRepository();

        public CategoryController(ICategoryRepository categoryRepository)
        {
            Repository = categoryRepository;
        }

        public IActionResult Index()
        {
            //ViewData["name"] = Request.Cookies["name"];
            //ViewData["name"] = TempData["tempname"];

            //ViewData["state"] = TempData["state"];

            var result = Repository.GetAll();
            return View(result);
        }

        //[HttpGet]
        public IActionResult Create()
        {
            Category category = new();
            return View(category);
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

            if(ModelState.IsValid)
            {
                Repository.CreateNew(category);
                TempData["state"] = "Add Category successfully";
                return RedirectToAction("Index");
            }

            return View(category);
        }

        public IActionResult Edit(int id)
        {
            var result = Repository.GetOne(id);
            return result != null ? View(result) : RedirectToAction("NotFound", "Home");
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            Repository.Edit(category);
            TempData["state"] = "Update Category successfully";

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var result = Repository.GetOne(id);

            if(result != null)
            {
                Repository.Delete(result);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("NotFound", "Home");
            }
        }
    }
}
