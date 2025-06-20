using FinalProjectConsume.Models.Tour;

namespace FinalProjectConsume.ViewModels.UI
{
    public class TourDetailVM
    {
        public Tour Tour { get; set; }
        public string SearchTerm { get; set; }

        public List<ReviewCreateVM> Reviews { get; set; } = new List<ReviewCreateVM>();

    }
}
