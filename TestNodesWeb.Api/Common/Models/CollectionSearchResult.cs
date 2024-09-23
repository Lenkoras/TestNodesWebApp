namespace TestNodesWeb.Api.Common.Models
{
    public class CollectionSearchResult<TResult>
    {
        public int TotalCount { get; set; }
        public ICollection<TResult> Collection { get; set; } = new List<TResult>();
    }
}