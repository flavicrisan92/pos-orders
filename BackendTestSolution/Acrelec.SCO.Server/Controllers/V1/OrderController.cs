using Acrelec.SCO.Server.Exceptions;
using Acrelec.SCO.Server.Interfaces;
using Acrelec.SCO.Server.Model.RestExchangedMessages;
using Acrelec.SCO.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Acrelec.SCO.Server.Controllers.V1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api-sco/v{version:apiVersion}")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrdersService _ordersService;

        public OrderController(ILogger<OrderController> logger, IOrdersService ordersService)
        {
            _logger = logger;
            _ordersService = ordersService;
        }

        /// <summary>
        /// Inject the order into the server. 
        /// </summary>
        /// <response code="200">Returns the order number.</response>
        /// <response code="400">If missing customer details</response>
        [Route("InjectOrder")]
        [HttpPost]
        [ProducesResponseType(typeof(InjectOrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult InjectOrder(InjectOrderRequest orderRequest)
        {
            try
            {
                ValidateOrder(orderRequest);
                string orderNumber = _ordersService.InjectOrder(orderRequest);
                InjectOrderResponse result = new()
                {
                    OrderNumber = orderNumber
                };
                return Ok(result);
            }
            catch (ValidationException exception)
            {
                _logger.LogError(exception.Message, exception);
                return BadRequest(exception.Message);
            }
            catch (Exception exception)
            {
                _logger.LogError("Inject new order failed", exception);
                return BadRequest(exception.Message);
            }
        }

        private void ValidateOrder(InjectOrderRequest orderRequest)
        {
            if (orderRequest.Order == null ||
    orderRequest.Order.OrderItems == null || orderRequest.Order.OrderItems.Count == 0 ||
    orderRequest.Order.OrderItems.Any(q => string.IsNullOrEmpty(q.ItemCode) || q.Qty == 0))
            {
                throw new ValidationException("Missing order details");
            }
            if (orderRequest.Customer == null)
            {
                throw new ValidationException("Missing customer details");
            }
        }
    }
}
