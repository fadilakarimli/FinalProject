using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.Instagram
{
    public class InstagramEdit
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please select an image")]
        public IFormFile Image { get; set; }
    }
}
