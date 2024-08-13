using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Project1.Models.ViewModel
{
	public class ApplicationUserVM
	{
        public int Id { get; set; }
		[Required]
		public string UserName { get; set; }
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Compare(nameof(Password))]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		[Required]
		public string Address { get; set; }

        public string Role { get; set; }
    }
}
