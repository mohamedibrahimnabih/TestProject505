using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
	public class Company
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //[EmailAddress]
        //[Display(Name = "Email Address")]
        //public string EmailAdress { get; set; }
    }
}
