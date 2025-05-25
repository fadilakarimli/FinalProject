using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.Brand
{
    public class BrandEdit
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please select an image")]
        public IFormFile Image { get; set; }
    }
}
