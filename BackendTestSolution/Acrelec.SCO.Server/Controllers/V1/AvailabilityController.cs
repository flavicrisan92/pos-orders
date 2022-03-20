using Acrelec.SCO.Server.Interfaces;
using Acrelec.SCO.Server.Model.RestExchangedMessages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Acrelec.SCO.Server.Controllers.V1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api-sco/v{version:apiVersion}/[controller]")]
    public class AvailabilityController : ControllerBase
    {
        private readonly IHearthbeatService _hearthBeatService;

        public AvailabilityController(IHearthbeatService hearthBeatService)
        {
            _hearthBeatService = hearthBeatService;
        }
        /// <summary>
        /// Check server availability
        /// </summary>
        /// <response code="200">Returns the server availability.</response>
        [ProducesResponseType(typeof(CheckAvailabilityResponse), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult Get()
        {
            var result = _hearthBeatService.CheckDependencies();
            return Ok(result);
        }
    }
}
