using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TestNodesWeb.Api.Features.Trees.Commands;
using TestNodesWeb.Api.Features.Trees.Models;

namespace TestNodesWeb.Controllers
{
    /// <summary>
    /// Represents entire tree API
    /// </summary>
    [ApiController]
    [Tags("user.tree")]
    public class TreeController : ApiControllerBase
    {
        public TreeController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// Returns your entire tree. If your tree doesn't exist it will be created automatically.
        /// </summary>
        [HttpPost("/api.user.tree.get")]
        [ProducesResponseType(typeof(MNode), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByNameAsync(
            [FromQuery][Required] string treeName)
        {
            return Ok(await Mediator.Send(new GetOrCreateTreeCommand()
            {
                Name = treeName
            }));
        }
    }
}