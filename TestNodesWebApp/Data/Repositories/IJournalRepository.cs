using Database;
using TestNodesWeb.Api.Common.Models;

namespace TestNodesWeb.Api.Data.Repositories
{
    public interface IJournalRepository
    {
        Task<CollectionSearchResult<JournalShortInfo>> SearchByTextAsync(JournalSearchConfig config,
            CancellationToken cancellationToken);

        Task AddAsync(Journal journal, CancellationToken cancellationToken);

        Task<Journal?> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}