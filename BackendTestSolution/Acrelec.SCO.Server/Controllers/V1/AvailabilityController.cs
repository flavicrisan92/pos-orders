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
        /// <summary>
        /// Check server availability
        /// </summary>
        /// <response code="200">Returns the server availability.</response>
        [ProducesResponseType(typeof(CheckAvailabilityResponse), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult Get()
        {
            CheckAvailabilityResponse result = CheckDependencies();
            return Ok(result);
        }

        private static CheckAvailabilityResponse CheckDependencies()
        {
            return new CheckAvailabilityResponse
            {
                CanInjectOrders = false
            };
        }
    }
}
