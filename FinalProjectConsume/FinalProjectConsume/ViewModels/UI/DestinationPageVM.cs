using FinalProjectConsume.Models.TrandingDestination;

namespace FinalProjectConsume.ViewModels.UI
{
    public class DestinationPageVM
    {
        public IEnumerable<TrandingDestination> TrandingDestinations { get; set; }
        public string SearchTerm { get; set; }

    }
}
