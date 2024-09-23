using MediatR;

namespace TestNodesWeb.Api.Features.Nodes.Commands
{
    public class DeleteNodeCommand : IRequest<Unit>
    {
        public string TreeName { get; set; }
        public int NodeId { get; set; }
    }
}