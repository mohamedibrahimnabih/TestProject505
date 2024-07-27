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

        public IActionResult CreateNew()
        {
            return View();
        }
        public IActionResult SaveNew(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();

            return RedirectToAction("CreateNew");
        }

        public IActionResult Edit(int id)
        {
            var result = context.Products.Find(id);
            return result != null ? View(result) : RedirectToAction("NotFound");
        }
        public IActionResult SaveEdit(Product product)
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
