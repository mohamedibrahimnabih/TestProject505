using Microsoft.AspNetCore.Mvc;
using Project1.Data;
using Project1.Models;
using Project1.Repository;
using Project1.Repository.IRepository;

namespace Project1.Controllers
{
	public class CompanyController : Controller
	{
		//ApplicationDbContext context = new ApplicationDbContext();

		private readonly ICompanyRepository companyRepository;// = new CompanyRepository();
        public CompanyController(ICompanyRepository companyRepository)
        {
			this.companyRepository = companyRepository;
        }

        public IActionResult Index() => View(companyRepository.GetAll());

		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(Company company)
		{
			if(ModelState.IsValid)
			{
				companyRepository.CreateNew(company);
				return RedirectToAction("Index");
			}
			return View(company);
		}

        public IActionResult Edit(int id) => View(companyRepository.GetOne(id));

        [HttpPost]
		[ValidateAntiForgeryToken]
        public IActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                companyRepository.Edit(company);
                return RedirectToAction("Index");
            }
            return View(company);
        }
    }
}
