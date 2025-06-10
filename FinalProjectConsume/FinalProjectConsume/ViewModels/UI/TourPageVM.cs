using FinalProjectConsume.Models.Activity;
using FinalProjectConsume.Models.Amenity;
using FinalProjectConsume.Models.City;
using FinalProjectConsume.Models.Tour;

namespace FinalProjectConsume.ViewModels.UI
{
    public class TourPageVM
    {
        public IEnumerable<Tour> Tours { get; set; }
        public IEnumerable<Amenity> Amenities { get; set; }
        public IEnumerable<Activity> Activities { get; set; }
        public IEnumerable<City> Cities { get; set; }
        public string? SearchTerm { get; set; }

    }
}
