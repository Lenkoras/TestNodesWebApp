using MediatR;

namespace TestNodesWeb.Api.Features.Nodes.Commands
{
    public class RenameNodeCommand : IRequest<Unit>
    {
        public string TreeName { get; set; } = string.Empty;
        public int NodeId { get; set; }
        public string NewNodeName { get; set; } = string.Empty;
    }
}