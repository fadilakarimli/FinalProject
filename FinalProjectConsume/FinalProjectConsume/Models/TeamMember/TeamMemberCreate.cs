using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.TeamMember
{
    public class TeamMemberCreate
    {
        [Required(ErrorMessage = "Full name is required")]
        [RegularExpression(@"^[A-Za-zƏəÖöÜüĞğÇçİıŞş\s]+$", ErrorMessage = "Full name must contain only letters and spaces")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Position is required")]
        [RegularExpression(@"^[A-Za-zƏəÖöÜüĞğÇçİıŞş\s]+$", ErrorMessage = "Position must contain only letters and spaces")]
        public string Position { get; set; }

        [Required(ErrorMessage = "Please select an image")]
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }
    }
}
