using System.ComponentModel.DataAnnotations;

namespace FinalProjectConsume.Models.DestinationFeature
{
    public class DestinationFeatureCreate
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Tour Count is required")]
        public int TourCount { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal PriceFrom { get; set; }

        [Required(ErrorMessage = "Please select an icon image")]
        public IFormFile IconImage { get; set; }
    }

}
