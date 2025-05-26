using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.TeamMember
{
    public class TeamMemberEdit
    {
        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Position is required")]
        public string Position { get; set; }

        // Image is optional on edit, user may not want to change the photo
        public IFormFile? Image { get; set; }
    }
}
