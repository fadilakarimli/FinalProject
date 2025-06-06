using System.ComponentModel.DataAnnotations;
using System.Numerics;
using PlanModel = FinalProjectConsume.Models.Plan;

namespace FinalProjectConsume.Models.Tour
{
    public class Tour
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
        public int CountryCount { get; set; }
        public string Desc { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public decimal? OldPrice { get; set; }
        public string ImageUrl { get; set; }
        public List<string> CityNames { get; set; }
        public List<string> ActivityNames { get; set; }
        public List<string> Amenities { get; set; }
        public List<string> ExperienceNames { get; set; }
        public List<PlanModel.Plan> Plans { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }


    }
}
