using Acrelec.SCO.Server.Controllers.V1;
using Acrelec.SCO.Server.Interfaces;
using Acrelec.SCO.Server.Model.RestExchangedMessages;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Acrelec.SCO.Server.Tests
{
    [TestClass]
    public class AvailabilityControllerTests
    {
        [TestMethod]
        public void AvailabilityController_WhenServerAvailable_CanInjectOrderShouldBeTrue()
        {
            var mockHearthbeatService = new Mock<IHearthbeatService>();
            mockHearthbeatService.Setup(q => q.CheckDependencies())
                .Returns(new CheckAvailabilityResponse(true));
            var controller = new AvailabilityController(mockHearthbeatService.Object);

            // Act
            var result = controller.Get();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var value = result.As<OkObjectResult>().Value.As<CheckAvailabilityResponse>();
            Assert.IsTrue(value.CanInjectOrders);
        }

        [TestMethod]
        public void AvailabilityController_WhenServerIsNotAvailable_CanInjectOrderShouldBeFalse()
        {
            var mockHearthbeatService = new Mock<IHearthbeatService>();
            mockHearthbeatService.Setup(q => q.CheckDependencies())
                .Returns(new CheckAvailabilityResponse(false));
            var controller = new AvailabilityController(mockHearthbeatService.Object);

            // Act
            var result = controller.Get();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var value = result.As<OkObjectResult>().Value.As<CheckAvailabilityResponse>();
            Assert.IsFalse(value.CanInjectOrders);
        }
    }
}
