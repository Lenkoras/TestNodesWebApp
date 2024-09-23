using AutoMapper;
using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestNodesWeb.Api.Data.Repositories;
using TestNodesWeb.Api.Features.Trees.Models;

namespace TestNodesWeb.Api.Features.Trees.Commands
{
    public class GetOrCreateTreeCommandHandler : IRequestHandler<GetOrCreateTreeCommand, MNode>
    {
        private readonly INodeRepository nodeRepository;
        private readonly IMapper mapper;

        public GetOrCreateTreeCommandHandler(INodeRepository nodeRepository,
            IMapper mapper)
        {
            this.nodeRepository = nodeRepository;
            this.mapper = mapper;
        }

        public async Task<MNode> Handle(GetOrCreateTreeCommand request,
            CancellationToken cancellationToken)
        {
            Node? tree = await nodeRepository.GetTreeByNameAsync(request.Name,
                trees => trees.Include(t => t.TreeNodes),
                cancellationToken)
                .ConfigureAwait(false);

            ICollection<Node> nodes;

            if (tree is null)
            {
                await nodeRepository.AddAsync(tree = new Node() { Name = request.Name },
                    cancellationToken)
                    .ConfigureAwait(false);
                nodes = Array.Empty<Node>();
            }
            else
            {
                nodes = tree.TreeNodes;
            }

            return GetNode(nodes, tree);
        }

        private static MNode GetNode(ICollection<Node> nodes, Node node)
        {
            MNode resultNode = new()
            {
                Id = node.Id,
                Name = node.Name,
                Children = nodes.Where(n => n.ParentNodeId == node.Id)
                    .Select(node => GetNode(nodes, node))
                    .ToList()
            };
            return resultNode;
        }
    }
}