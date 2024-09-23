using Database;
using MediatR;

namespace TestNodesWeb.Api.Features.Journals.Queries
{
    public class GetJournalByIdQuery : IRequest<Journal?>
    {
        public int Id { get; set; }
    }
}