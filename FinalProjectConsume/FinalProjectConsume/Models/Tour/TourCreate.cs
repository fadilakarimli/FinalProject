using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.Tour
{
    public class TourCreate
    {
        [Required]
        public string Name { get; set; }
        public string Duration { get; set; }
        public int CountryCount { get; set; }
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public int CityId { get; set; }
        public IFormFile ImageFile { get; set; }
        public List<int> ActivityIds { get; set; } = new List<int>();
        public List<int> AmenityIds { get; set; } = new List<int>();
    }
}
