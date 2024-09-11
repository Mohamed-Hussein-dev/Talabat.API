using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0.1, int.MaxValue , ErrorMessage = "Price Can't be Zero")]
        public decimal Price { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        [Range(1 , int.MaxValue , ErrorMessage = "You Should buy at least one item")]
        public int Quantity { get; set; }
        [Required]
        public string PicturUrl { get; set; }
    }
}
