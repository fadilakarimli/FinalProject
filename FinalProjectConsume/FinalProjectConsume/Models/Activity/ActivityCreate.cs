using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.Activity
{
    public class ActivityCreate
    {
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Only spaces and letters")]
        public string Name { get; set; }

    }
}
