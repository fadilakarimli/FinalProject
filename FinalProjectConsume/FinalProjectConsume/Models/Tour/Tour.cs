namespace FinalProjectConsume.Models.Tour
{
    public class Tour
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
        public int CountryCount { get; set; }
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public string ImageUrl { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public List<string> ActivityNames { get; set; }
        public List<string> Amenities { get; set; }
    }
}
