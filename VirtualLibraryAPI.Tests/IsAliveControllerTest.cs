using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Library.Controllers;

namespace VirtualLibraryAPI.Tests
{
    public class IsAliveControllerTest
    {
        private readonly ILogger<IsAliveController> _logger;

        public IsAliveControllerTest()
        {
            _logger = new Mock<ILogger<IsAliveController>>().Object;
        }

        [Fact]
        public void GetResult_ReturnsOk()
        {
            var controller = new IsAliveController(_logger);

            var result = controller.GetResult();

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.Equal("I'm alive", okResult.Value);
        }
    }
}
