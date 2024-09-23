using MediatR;
using TestNodesWeb.Api.Features.Trees.Models;

namespace TestNodesWeb.Api.Features.Trees.Commands
{
    public class GetOrCreateTreeCommand : IRequest<MNode>
    {
        public string Name { get; set; }
    }
}