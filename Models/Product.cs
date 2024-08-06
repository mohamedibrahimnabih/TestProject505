using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; }

        [RegularExpression(@"[0-9]+\.(jpg|png)", ErrorMessage = "your photos must be suffix with .png or .jpg")]
        public string? Img { get; set; }
        [Range(1, 500000)]
        public double Price { get; set; }
        [Range(1, 5)]
        public int Rate { get; set; }

        public int CategoryId { get; set; }
        //[ForeignKey(nameof(CategoryId))]
        [ValidateNever]
        public Category Category { get; set; }
    }
}
