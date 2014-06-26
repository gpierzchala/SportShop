using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SportShop.Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Product name is required")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Product description is required")]
        public string Description { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Product price is required")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Product category is required")]
        public string Category { get; set; }
        public byte[] ImageData { get; set; }
        
        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }
    }
}
