using Database;

namespace TestNodesWeb.Api.Data.Repositories
{
    public interface INodeRepository
    {
        Task AddAsync(Node node, CancellationToken cancellationToken);
        Task<Node?> GetByIdAsync(int id,
            Func<IQueryable<Node>, IQueryable<Node>> query,
            CancellationToken cancellationToken);
        Task<Node?> GetTreeByNameAsync(string treeName,
            Func<IQueryable<Node>, IQueryable<Node>> query,
            CancellationToken cancellationToken);
        Task<int> DeleteByIdAsync(int id,
            CancellationToken cancellationToken);
        Task<int> RenameAsync(int id, string newName,
            CancellationToken cancellationToken);
    }
}