namespace TestNodesWeb.Api.Features.Journals.Queries.Models
{
    public class MJournalInfo
    {
        public int Id { get; set; }
        public string EventId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}