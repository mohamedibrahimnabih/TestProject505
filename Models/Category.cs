using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "this is required")]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        [ValidateNever]
        public ICollection<Product> Products { get; set; }
    }
}
