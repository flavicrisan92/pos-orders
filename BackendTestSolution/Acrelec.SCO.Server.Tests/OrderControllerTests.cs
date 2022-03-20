using Acrelec.SCO.Server.Controllers.V1;
using Acrelec.SCO.Server.Interfaces;
using Acrelec.SCO.Server.Model.RestExchangedMessages;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acrelec.SCO.Server.Tests
{
    [TestClass]
    public class OrderControllerTests
    {
        [TestMethod]
        public void OrderController_WhenValidOrder_ReturnOrderNumber()
        {
            var mockOrderService = new Mock<IOrdersService>();
            mockOrderService.Setup(q => q.InjectOrder(It.IsAny<InjectOrderRequest>()))
                .Returns("10");
            var mockLogger = new Mock<ILogger<OrderController>>();
            var controller = new OrderController(mockLogger.Object, mockOrderService.Object);

            var order = new InjectOrderRequest()
            {
                Customer = new DataStructures.Customer(),
                Order = new DataStructures.Order()
                {
                    OrderItems = new List<DataStructures.OrderedItem>()
                    {
                        new DataStructures.OrderedItem()
                        {
                            ItemCode = "100",
                            Qty = 1
                        },
                        new DataStructures.OrderedItem()
                        {
                            ItemCode= "200",
                            Qty = 2
                        }
                    }
                }
            };

            // Act
            var result = controller.InjectOrder(order);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            InjectOrderResponse value = result.As<OkObjectResult>().Value.As<InjectOrderResponse>();
            Assert.IsNotNull(value);
            Assert.AreEqual("10", value.OrderNumber);
        }

        [TestMethod]
        public void OrderController_WhenInvalidOrder_ReturnBadRequest()
        {
            var mockOrderService = new Mock<IOrdersService>();
            mockOrderService.Setup(q => q.InjectOrder(It.IsAny<InjectOrderRequest>()))
                .Returns("10");
            var mockLogger = new Mock<ILogger<OrderController>>();
            var controller = new OrderController(mockLogger.Object, mockOrderService.Object);

            var order = new InjectOrderRequest()
            {
                Customer = new DataStructures.Customer()
            };

            // Act
            var result = controller.InjectOrder(order);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var value = result.As<BadRequestObjectResult>().Value.As<string>();
            Assert.AreEqual("Missing order details", value);
        }

        [TestMethod]
        public void OrderController_WhenInvalidOrderItems_ReturnBadRequest()
        {
            var mockOrderService = new Mock<IOrdersService>();
            mockOrderService.Setup(q => q.InjectOrder(It.IsAny<InjectOrderRequest>()))
                .Returns("10");
            var mockLogger = new Mock<ILogger<OrderController>>();
            var controller = new OrderController(mockLogger.Object, mockOrderService.Object);

            var order = new InjectOrderRequest()
            {
                Customer = new DataStructures.Customer(),
                Order = new DataStructures.Order()
            };

            // Act
            var result = controller.InjectOrder(order);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var value = result.As<BadRequestObjectResult>().Value.As<string>();
            Assert.AreEqual("Missing order details", value);
        }

        [TestMethod]
        public void OrderController_WhenInvalidCustomer_ReturnBadRequest()
        {
            var mockOrderService = new Mock<IOrdersService>();
            mockOrderService.Setup(q => q.InjectOrder(It.IsAny<InjectOrderRequest>()))
                .Returns("10");
            var mockLogger = new Mock<ILogger<OrderController>>();
            var controller = new OrderController(mockLogger.Object, mockOrderService.Object);

            var order = new InjectOrderRequest()
            {
                Order = new DataStructures.Order()
                {
                    OrderItems = new List<DataStructures.OrderedItem>()
                    {
                        new DataStructures.OrderedItem()
                        {
                            ItemCode = "100",
                            Qty = 1
                        },
                        new DataStructures.OrderedItem()
                        {
                            ItemCode= "200",
                            Qty = 2
                        }
                    }
                }
            };

            // Act
            var result = controller.InjectOrder(order);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var value = result.As<BadRequestObjectResult>().Value.As<string>();
            Assert.AreEqual("Missing customer details", value);
        }
    }
}
