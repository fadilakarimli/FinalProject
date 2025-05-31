using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.AboutAgency
{
    public class AboutAgencyEdit
    {
        [Required(ErrorMessage = "Please select an image")]
        public IFormFile Image { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Subtitle { get; set; }
    }
}
