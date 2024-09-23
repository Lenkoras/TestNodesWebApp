namespace TestNodesWeb.Api.Common.Models
{
    public class MRange<T>
        where T : class
    {
        public int Skip { get; set; }
        public int Count { get; set; }
        public ICollection<T> Items { get; set; } = new List<T>();
    }
}