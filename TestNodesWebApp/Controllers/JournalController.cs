using Database;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TestNodesWeb.Api.Common.Models;
using TestNodesWeb.Api.Features.Journals.Queries;
using TestNodesWeb.Api.Features.Journals.Queries.Models;

namespace TestNodesWeb.Controllers
{
    /// <summary>
    /// Represents journal API
    /// </summary>
    [ApiController]
    [Tags("user.journal")]
    public class JournalController : ApiControllerBase
    {
        public JournalController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// Provides the pagination API.
        /// Skip means the number of items should be skipped by server.
        /// Take means the maximum number items should be returned by server.
        /// All fields of the filter are optional.
        /// </summary>
        [HttpPost("/api.user.journal.getRange")]
        [ProducesResponseType(typeof(MRange<MJournalInfo>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRangeAsync([FromQuery][Required] int skip,
            [FromQuery][Required] int take,
            [FromBody][Required] VJournalFilter filter)
        {
            return Ok(await Mediator.Send(new GetJournalRangeQuery()
            {
                Config = new()
                {
                    Text = filter?.Search,
                    Pagination = new(skip, take),
                    From = filter?.From,
                    To = filter?.To
                }
            }));
        }

        /// <summary>
        /// Returns the information about an particular event by ID.
        /// </summary>
        [HttpPost("/api.user.journal.getSingle")]
        [ProducesResponseType(typeof(Journal), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdAsync([FromQuery][Required] int id)
        {
            return Ok(await Mediator.Send(new GetJournalByIdQuery()
            {
                Id = id
            }));
        }
    }
}