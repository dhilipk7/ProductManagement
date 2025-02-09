using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Api.Models
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters.")]
        public string Name { get; set; }

        [StringLength(255, ErrorMessage = "Description can be up to 255 characters.")]
        public string? Description { get; set; }

        [Range(1.0, 10000000.00, ErrorMessage = "Price must be a positive number")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue,ErrorMessage = "Stock must be a positive number.")]
        public int Stock { get; set; }
    }

}
