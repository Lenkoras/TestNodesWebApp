using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TestNodesWeb.Api.Features.Nodes.Commands;

namespace TestNodesWeb.Controllers
{
    /// <summary>
    /// Represents tree node API
    /// </summary>
    [ApiController]
    [Tags("user.tree.node")]
    public class NodeController : ApiControllerBase
    {
        public NodeController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// Create a new node in your tree.
        /// You must to specify a parent node ID that belongs to your tree.
        /// A new node name must be unique across all siblings.
        /// </summary>
        [HttpPost("/api.user.tree.node.create")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByNameAsync(
            [FromQuery][Required] string treeName,
            [FromQuery][Required] int nodeId,
            [FromQuery][Required] string nodeName)
        {
            await Mediator.Send(new CreateNodeCommand()
            {
                TreeName = treeName,
                ParentNodeId = nodeId,
                NodeName = nodeName
            }).ConfigureAwait(false);
            return Ok();
        }

        /// <summary>
        /// Delete an existing node in your tree.
        /// You must specify a node ID that belongs your tree.
        /// </summary>
        [HttpPost("/api.user.tree.node.delete")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteNodeByIdAsync(
            [FromQuery][Required] string treeName,
            [FromQuery][Required] int nodeId)
        {
            await Mediator.Send(new DeleteNodeCommand()
            {
                TreeName = treeName,
                NodeId = nodeId
            }).ConfigureAwait(false);
            return Ok();
        }

        /// <summary>
        /// Rename an existing node in your tree.
        /// You must specify a node ID that belongs your tree.
        /// A new name of the node must be unique across all siblings.
        /// </summary>
        [HttpPost("/api.user.tree.node.rename")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> RenameNodeByIdAsync(
            [FromQuery][Required] string treeName,
            [FromQuery][Required] int nodeId,
            [FromQuery][Required] string newNodeName)
        {
            await Mediator.Send(new RenameNodeCommand()
            {
                TreeName = treeName,
                NodeId = nodeId,
                NewNodeName = newNodeName
            }).ConfigureAwait(false);
            return Ok();
        }
    }
}