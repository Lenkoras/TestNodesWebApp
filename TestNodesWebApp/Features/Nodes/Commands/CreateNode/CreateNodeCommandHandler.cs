using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestNodesWeb.Api.Data.Repositories;

namespace TestNodesWeb.Api.Features.Nodes.Commands
{
    public class CreateNodeCommandHandler : IRequestHandler<CreateNodeCommand, Unit>
    {
        private readonly INodeRepository nodeRepository;

        public CreateNodeCommandHandler(INodeRepository nodeRepository)
        {
            this.nodeRepository = nodeRepository;
        }

        public async Task<Unit> Handle(CreateNodeCommand request,
            CancellationToken cancellationToken)
        {
            Node parentNode = await nodeRepository.GetByIdAsync(request.ParentNodeId,
                nodes => nodes.Include(n => n.Tree)
                    .Include(n => n.Children),
                cancellationToken)
                .ConfigureAwait(false)
                ?? throw new SecureException(
                    $"Node with ID = {request.ParentNodeId} was not found");
            Node? tree = parentNode.Tree ?? parentNode;
            if (tree.Name != request.TreeName)
            {
                throw new SecureException(
                    "Requested node was found, but it doesn't belong your tree");
            }
            if (parentNode.Children.Any(n => n.Name == request.NodeName))
            {
                throw new SecureException("Duplicate name");
            }

            await nodeRepository.AddAsync(
                new()
                {
                    Name = request.NodeName,
                    Tree = tree,
                    ParentNode = parentNode
                }, cancellationToken)
                .ConfigureAwait(false);

            return Unit.Value;
        }
    }
}