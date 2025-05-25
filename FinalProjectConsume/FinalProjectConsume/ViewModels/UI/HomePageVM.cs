using FinalProjectConsume.Models.Brand;
using FinalProjectConsume.Models.Instagram;

namespace FinalProjectConsume.ViewModels.UI
{
    public class HomePageVM
    {
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<Instagram> Instagrams { get; set; }
    }
}
