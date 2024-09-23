using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TestNodesWeb.Controllers
{
    [ApiController]
    [Tags("user.partner")]
    public class PartnerController : ControllerBase
    {
        [HttpPost("/api.user.partner.rememberMe")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public IActionResult RememberPartner([FromQuery][Required] string code)
        {
            HttpContext.Response.Cookies.Append("Code", code, new CookieOptions()
            {
                Expires = DateTime.UtcNow.AddMonths(1)
            });
            return Ok();
        }
    }
}