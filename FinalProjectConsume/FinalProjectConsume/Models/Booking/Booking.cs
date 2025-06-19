namespace FinalProjectConsume.Models.Booking
{
    public class Booking
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public int AdultsCount { get; set; }
        public int ChildrenCount { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
