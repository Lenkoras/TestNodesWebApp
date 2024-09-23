namespace TestNodesWeb.Api.Common.Models
{
    public class JournalSearchConfig
    {
        public string? Text { get; set; } = string.Empty;
        public PaginationInfo Pagination { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}