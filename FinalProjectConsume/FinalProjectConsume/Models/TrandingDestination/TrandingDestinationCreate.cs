using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.TrandingDestination
{
    public class TrandingDestinationCreate
    {
        [Required(ErrorMessage = "Title is required")]
        [RegularExpression(@"^[A-Za-zƏəÖöÜüĞğÇçİıŞş\s]+$",
               ErrorMessage = "Only letter and space")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please select an image")]
        public IFormFile Image { get; set; }
    }
}
