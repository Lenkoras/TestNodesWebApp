using Database;
using MediatR;
using TestNodesWeb.Api.Data.Repositories;

namespace TestNodesWeb.Api.Features.Journals.Queries
{
    public class GetJournalByIdQueryHandler : IRequestHandler<GetJournalByIdQuery, Journal?>
    {
        private readonly IJournalRepository journalRepository;

        public GetJournalByIdQueryHandler(IJournalRepository journalRepository)
        {
            this.journalRepository = journalRepository;
        }

        public async Task<Journal?> Handle(GetJournalByIdQuery request,
            CancellationToken cancellationToken) =>
            await journalRepository.GetByIdAsync(request.Id, cancellationToken)
                .ConfigureAwait(false);
    }
}