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
        public int Capacity { get; set; }
        public List<int> CityIds { get; set; } = new List<int>();
        public IFormFile ImageFile { get; set; }
        public List<int> ActivityIds { get; set; }
        public List<int> AmenityIds { get; set; } = new List<int>();
        public List<int> CountryIds { get; set; }
        public string Desc { get; set; }
        //public List<int> ExperienceIds { get; set; } = new List<int>();


        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }
    }
}
