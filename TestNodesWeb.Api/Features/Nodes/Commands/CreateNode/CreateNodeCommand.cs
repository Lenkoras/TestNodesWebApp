using MediatR;

namespace TestNodesWeb.Api.Features.Nodes.Commands
{
    public class CreateNodeCommand : IRequest<Unit>
    {
        public string TreeName { get; set; } = string.Empty;
        public int ParentNodeId { get; set; }
        public string NodeName { get; set; } = string.Empty;
    }
}