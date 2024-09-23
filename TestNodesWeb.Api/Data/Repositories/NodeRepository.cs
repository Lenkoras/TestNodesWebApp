using Database;
using Microsoft.EntityFrameworkCore;

namespace TestNodesWeb.Api.Data.Repositories
{
    public class NodeRepository : INodeRepository
    {
        private readonly ApplicationDbContext dbContext;

        public NodeRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Node?> GetByIdAsync(int id,
            Func<IQueryable<Node>, IQueryable<Node>> query,
        CancellationToken cancellationToken)
        {
            return await query(dbContext.Nodes).FirstOrDefaultAsync(n => n.Id == id,
                cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task AddAsync(Node node,
            CancellationToken cancellationToken)
        {
            dbContext.Nodes.Add(node);
            await dbContext.SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<Node?> GetTreeByNameAsync(string treeName,
            Func<IQueryable<Node>, IQueryable<Node>> query,
            CancellationToken cancellationToken)
        {
            return await query(dbContext.Nodes)
                .FirstOrDefaultAsync(
                tree => tree.ParentNodeId == null &&
                    tree.Name == treeName,
                cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<int> DeleteByIdAsync(int id,
            CancellationToken cancellationToken)
        {
            return await dbContext.Nodes.Where(n => n.Id == id)
                .ExecuteDeleteAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<int> RenameAsync(int id, string newName,
            CancellationToken cancellationToken)
        {
            return await dbContext.Nodes.Where(n => n.Id == id)
                .ExecuteUpdateAsync(calls =>
                    calls.SetProperty(x => x.Name, x => newName),
                cancellationToken)
                .ConfigureAwait(false);
        }
    }
}