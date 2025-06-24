using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.Experience
{
    public class ExperienceCreate
    {
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Only spaces and letters")]

        public string Name { get; set; }
        public int TourId { get; set; }
    }
}
