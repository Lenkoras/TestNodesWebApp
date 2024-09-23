using MediatR;
using TestNodesWeb.Api.Common.Models;
using TestNodesWeb.Api.Features.Journals.Queries.Models;

namespace TestNodesWeb.Api.Features.Journals.Queries
{
    public class GetJournalRangeQuery : IRequest<MRange<MJournalInfo>>
    {
        public JournalSearchConfig Config { get; set; } = null!;
    }
}