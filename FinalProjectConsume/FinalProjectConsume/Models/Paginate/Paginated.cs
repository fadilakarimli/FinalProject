namespace FinalProjectConsume.Models.Paginate
{
    public class Paginated<T>
    {
        public List<T> Items { get; set; }
        public int PageCount { get; set; }
        public int TotalPages { get; set; }
    }
}
