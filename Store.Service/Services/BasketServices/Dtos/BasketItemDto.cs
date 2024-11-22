using System.ComponentModel.DataAnnotations;

namespace Store.Service.Services.BasketServices.Dtos
{
    public class BasketItemDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }

        [Required]
        
        public string ProductName { get; set; }
        [Required]
        [Range(0.1, double.MaxValue,ErrorMessage ="Price Must Be Greater than 0")]

        public decimal Price { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Quantity Must Be Between 1 and 10")]

        public int Quantity { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]

        public string BrandName { get; set; }
        [Required]

        public string TypeName { get; set; }
    }
}