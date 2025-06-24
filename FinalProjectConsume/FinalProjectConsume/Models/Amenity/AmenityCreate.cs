using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.Amenity
{
    public class AmenityCreate
    {
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Only spaces and letters")]

        public string Name { get; set; }
    }
}
