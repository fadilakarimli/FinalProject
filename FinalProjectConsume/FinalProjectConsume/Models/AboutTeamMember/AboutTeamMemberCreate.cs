namespace FinalProjectConsume.Models.AboutTeamMember
{
    public class AboutTeamMemberCreate
    {
        public IFormFile Image { get; set; }
        public string Position { get; set; }
        public string FullName { get; set; }
        public string About { get; set; }
    }
}

