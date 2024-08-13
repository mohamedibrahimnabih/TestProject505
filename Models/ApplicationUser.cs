using Microsoft.AspNetCore.Identity;

namespace Project1.Models
{
	public class ApplicationUser : IdentityUser
	{
        public string Address { get; set; }
    }
}
