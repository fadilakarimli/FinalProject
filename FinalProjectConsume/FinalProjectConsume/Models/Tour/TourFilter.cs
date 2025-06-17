namespace FinalProjectConsume.Models.Tour
{
    public class TourFilter
    {
        public List<string> CityNames { get; set; }
        public List<string> ActivityNames { get; set; }
        public DateTime? DepartureDate { get; set; }
        public int? GuestCount { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public List<string> AmenityNames { get; set; }
    }
}
