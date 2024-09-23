using Database;
using Microsoft.EntityFrameworkCore;
using TestNodesWeb.Api.Common.Models;

namespace TestNodesWeb.Api.Data.Repositories
{
    public class JournalRepository : IJournalRepository
    {
        private readonly ApplicationDbContext dbContext;

        public JournalRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<CollectionSearchResult<JournalShortInfo>> SearchByTextAsync(
            JournalSearchConfig config,
            CancellationToken cancellationToken)
        {
            IQueryable<Journal> query = dbContext.Journals.OrderBy(j => j.Id);
            if (config.From != null)
            {
                query = query.Where(journal => journal.CreatedAt >= config.From);
            }
            if (config.To != null)
            {
                query = query.Where(journal => journal.CreatedAt <= config.To);
            }
            if (config.Text != null)
            {
                query = query.Where(journal => EF.Functions.ILike(journal.Text, $"%{config.Text}%"));
            }

            var totalNumberOfJournalsFound = await query.CountAsync(cancellationToken)
                .ConfigureAwait(false);

            var journals = await query.Paginate(config.Pagination)
                .Select(j => new JournalShortInfo()
                {
                    Id = j.Id,
                    EventId = j.EventId,
                    CreatedAt = j.CreatedAt
                })
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return new()
            {
                TotalCount = totalNumberOfJournalsFound,
                Collection = journals
            };
        }

        public async Task AddAsync(Journal journal, CancellationToken cancellationToken)
        {
            dbContext.Journals.Add(journal);
            await dbContext.SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<Journal?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await dbContext.Journals.FindAsync(id, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}