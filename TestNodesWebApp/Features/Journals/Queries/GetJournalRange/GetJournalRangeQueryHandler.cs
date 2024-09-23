using AutoMapper;
using MediatR;
using TestNodesWeb.Api.Common.Models;
using TestNodesWeb.Api.Data.Repositories;
using TestNodesWeb.Api.Features.Journals.Queries.Models;

namespace TestNodesWeb.Api.Features.Journals.Queries
{
    public class GetJournalRangeQueryHandler : IRequestHandler<GetJournalRangeQuery, MRange<MJournalInfo>>
    {
        private readonly IJournalRepository journalRepository;
        private readonly IMapper mapper;

        public GetJournalRangeQueryHandler(IJournalRepository journalRepository,
            IMapper mapper)
        {
            this.journalRepository = journalRepository;
            this.mapper = mapper;
        }

        public async Task<MRange<MJournalInfo>> Handle(GetJournalRangeQuery request, CancellationToken cancellationToken)
        {
            var result = await journalRepository.SearchByTextAsync(request.Config, cancellationToken)
                .ConfigureAwait(false);
            return new()
            {
                Skip = request.Config.Pagination.SkipCount,
                Count = result.TotalCount,
                Items = mapper.Map<ICollection<JournalShortInfo>, ICollection<MJournalInfo>>(result.Collection)
            };
        }
    }
}