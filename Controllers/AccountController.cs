using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Project1.Models;
using Project1.Models.ViewModel;

namespace Project1.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

		public IActionResult Register()
		{
            if (User.IsInRole("Admin"))
			{
				var result = roleManager.Roles.Select(e => new SelectListItem
				{
					Value = e.Name,
					Text = e.Name
				});
				ViewBag.Roles = result;
			}
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(ApplicationUserVM userVM)
		{
			if (ModelState.IsValid)
			{
				ApplicationUser user = new()
				{
					UserName = userVM.UserName,
					Email = userVM.Email,
					Address = userVM.Address,
				};

				var result = await userManager.CreateAsync(user, userVM.Password);
                if(result.Succeeded)
				{
					if(User.IsInRole("Admin"))
                        await userManager.AddToRoleAsync(user, userVM.Role);
					else
						await userManager.AddToRoleAsync(user, "Customer");

                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
				}

				ModelState.AddModelError("Password", "Don't match the constrains");
			}

			return View(userVM);
		}

		public IActionResult Login()
		{
			if(roleManager.Roles.IsNullOrEmpty())
			{
				roleManager.CreateAsync(new("Admin"));
				roleManager.CreateAsync(new("Customer"));
				roleManager.CreateAsync(new("Employee"));
				roleManager.CreateAsync(new("Company"));
			}

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginVM loginVM)
		{
			if (ModelState.IsValid)
			{
				var user = await userManager.FindByEmailAsync(loginVM.Email);

				if(user != null)
				{
					var result = await userManager.CheckPasswordAsync(user, loginVM.Password);

					if(result)
					{
						// login, Create ID, Create Cookie
						await signInManager.SignInAsync(user, loginVM.RememberMe);
						return RedirectToAction("Index", "Home");
					}
					else
						ModelState.AddModelError("Password", "incorrect password");
				}
				else
					ModelState.AddModelError("Email", "incorrect email");
			}
			return View(loginVM);
		}

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(RoleVM roleVM)
        {
			if (ModelState.IsValid)
			{
				IdentityRole user = new(roleVM.Name);

				await roleManager.CreateAsync(user);
				return RedirectToAction("CreateRole");
            }

			return View(roleVM);
        }

		public IActionResult AccessDenied()
		{
			return RedirectToAction("NotFound", "Home");
		}

        public IActionResult Logout()
		{
			signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
