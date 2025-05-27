namespace FinalProjectConsume.Models.Tour
{
    public class TourEdit
    {
        public string Name { get; set; }

        public string Duration { get; set; }

        public int CountryCount { get; set; }

        public decimal Price { get; set; }

        public decimal? OldPrice { get; set; }

        public int CityId { get; set; }

        public IFormFile? ImageFile { get; set; }

        public string? ExistingImageUrl { get; set; }
    }
}
