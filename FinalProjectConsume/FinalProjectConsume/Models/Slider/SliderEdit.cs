using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.Slider
{
    public class SliderEdit
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please select an image")]
        public IFormFile Img { get; set; }
    }
}
