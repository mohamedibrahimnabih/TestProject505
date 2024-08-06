using Microsoft.AspNetCore.Mvc;
using Project1.Data;
using Project1.Models;

namespace Project1.Controllers
{
	public class CompanyController : Controller
	{
		ApplicationDbContext context = new ApplicationDbContext();
		public IActionResult Index() => View(context.Companies.ToList());

		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(Company company)
		{
			if(ModelState.IsValid)
			{
				context.Companies.Add(company);
				context.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(company);
		}

        public IActionResult Edit(int id) => View(context.Companies.Find(id));

        [HttpPost]
		[ValidateAntiForgeryToken]
        public IActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                context.Companies.Update(company);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company);
        }
    }
}
