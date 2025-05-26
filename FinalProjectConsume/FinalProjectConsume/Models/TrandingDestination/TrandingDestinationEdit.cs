using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.TrandingDestination
{
    public class TrandingDestinationEdit
    {
        public string Title { get; set; }
        [Required(ErrorMessage = "Please select an image")]
        public IFormFile Image { get; set; }
    }
}
