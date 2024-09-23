namespace TestNodesWeb.Api.Features.Journals.Queries.Models
{
    public class VJournalFilter
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string? Search { get; set; }
    }
}