using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestNodesWeb.Api.Data.Repositories;

namespace TestNodesWeb.Api.Features.Nodes.Commands
{
    public class DeleteNodeCommandHandler : IRequestHandler<DeleteNodeCommand, Unit>
    {
        private readonly INodeRepository nodeRepository;

        public DeleteNodeCommandHandler(INodeRepository nodeRepository)
        {
            this.nodeRepository = nodeRepository;
        }

        public async Task<Unit> Handle(DeleteNodeCommand request, CancellationToken cancellationToken)
        {
            Node node = await nodeRepository.GetByIdAsync(request.NodeId,
                nodes => nodes.Include(n => n.Tree)
                    .Include(n => n.Children),
                cancellationToken)
                .ConfigureAwait(false)
                ?? throw new SecureException(
                    $"Node with ID = {request.NodeId} was not found");
            Node? tree = node.Tree ?? node;
            if (tree.Name != request.TreeName)
            {
                throw new SecureException(
                    "Requested node was found, but it doesn't belong your tree");
            }
            if (node.Children.Any())
            {
                throw new SecureException(
                    "You have to delete all children nodes first");
            }
            await nodeRepository.DeleteByIdAsync(node.Id, cancellationToken)
                .ConfigureAwait(false);

            return Unit.Value;
        }
    }
}