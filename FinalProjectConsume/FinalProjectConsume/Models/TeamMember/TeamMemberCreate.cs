using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.TeamMember
{
    public class TeamMemberCreate
    {
        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Position is required")]
        public string Position { get; set; }

        [Required(ErrorMessage = "Please select an image")]
        public IFormFile Image { get; set; }
    }
}
