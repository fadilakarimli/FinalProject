using FinalProjectConsume.Models.Blog;
using FinalProjectConsume.Models.Brand;
using FinalProjectConsume.Models.DestinationFeature;
using FinalProjectConsume.Models.Instagram;
using FinalProjectConsume.Models.NewsLetter;
using FinalProjectConsume.Models.Slider;
using FinalProjectConsume.Models.TeamMember;
using FinalProjectConsume.Models.Tour;
using FinalProjectConsume.Models.TrandingDestination;

namespace FinalProjectConsume.ViewModels.UI
{
    public class HomePageVM
    {
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<Instagram> Instagrams { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<DestinationFeature> DestinationFeatures { get; set; }
        public IEnumerable<TeamMember> TeamMembersB { get; set; }
        public IEnumerable<TrandingDestination> TrandingDestinations { get; set; }
        public IEnumerable<Tour> Tours { get; set; }
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<NewsLetter> NewsLetters { get; set; }
    }
}
