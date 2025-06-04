using FinalProjectConsume.Models.AboutApp;
using FinalProjectConsume.Models.AboutTeamMember;
using FinalProjectConsume.Models.Brand;
using FinalProjectConsume.Models.ChooseUsAbout;
using FinalProjectConsume.Models.TeamMember;

namespace FinalProjectConsume.ViewModels.UI
{
    public class AboutPageVM
    {
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<ChooseUsAbout> ChooseUsAbouts { get; set; }
        public IEnumerable<AboutTeamMember> AboutTeamMembers { get; set; }
        public IEnumerable<AboutApp> AboutApps { get; set; }

    }
}
