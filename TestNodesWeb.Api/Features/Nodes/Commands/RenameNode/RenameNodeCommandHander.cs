using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestNodesWeb.Api.Data.Repositories;

namespace TestNodesWeb.Api.Features.Nodes.Commands
{
    public class RenameNodeCommandHander : IRequestHandler<RenameNodeCommand, Unit>
    {
        private readonly INodeRepository nodeRepository;

        public RenameNodeCommandHander(INodeRepository nodeRepository)
        {
            this.nodeRepository = nodeRepository;
        }

        public async Task<Unit> Handle(RenameNodeCommand request, CancellationToken cancellationToken)
        {
            Node node = await nodeRepository.GetByIdAsync(request.NodeId,
                nodes => nodes.Include(n => n.Tree)
                    .Include(n => n.ParentNode)
                    .ThenInclude(parentNode => parentNode!.Children),
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
            if (node.ParentNode == null)
            {
                throw new SecureException("Couldn't rename root node");
            }
            if (node.Name == request.NewNodeName)
            {
                return Unit.Value;
            }
            if (node.ParentNode.Children.Any(n => n.Name == request.NewNodeName))
            {
                throw new SecureException("Duplicate name");
            }

            await nodeRepository.RenameAsync(node.Id,
                request.NewNodeName,
                cancellationToken)
                .ConfigureAwait(false);

            return Unit.Value;
        }
    }
}