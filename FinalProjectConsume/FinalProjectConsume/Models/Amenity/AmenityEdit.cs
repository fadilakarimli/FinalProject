using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.Amenity
{
    public class AmenityEdit
    {
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Only spaces and letters")]
        public string Name { get; set; }
    }
}
