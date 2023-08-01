using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Library.Controllers;
using VirtualLibraryAPI.Models;

namespace VirtualLibraryAPI.Tests
{
    public class ClientControllerTest
    {
        private readonly Mock<ILogger<ClientController>> _logger;
        private readonly Mock<IClientModel> _clientModel;
        private readonly ClientController _controller;

        public ClientControllerTest()
        {
            _logger = new Mock<ILogger<ClientController>>();
            _clientModel = new Mock<IClientModel>();
            _controller = new ClientController(_logger.Object, _clientModel.Object);
        }


        [Fact]
        public void GettAllClients_ReturnOK()
        {
            var clients = new List<Domain.DTOs.Client>
        {
            new Domain.DTOs.Client { ClientID = 1, FirstName = "20/20223" },
            new Domain.DTOs.Client { ClientID = 2, FirstName = "20/20223" },
            new Domain.DTOs.Client { ClientID = 3, FirstName = "20/20223" }
        };
            _clientModel.Setup(model => model.GetAllClients()).Returns(clients);


            var result = _controller.GetAllClients();

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void GettAllClients_ReturnNotFound()
        {
            List<Domain.DTOs.Client> clients = null;
            _clientModel.Setup(m => m.GetAllClients()).Returns(clients);

            var result = _controller.GetAllClients();

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void GettAllClients_ReturnbBadRequest()
        {
            _clientModel.Setup(model => model.GetAllClients()).Throws(new Exception("Failed to retrieve magazines"));

            var result = _controller.GetAllClients();

            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed", badRequestResult.Value);
        }
        [Fact]
        public void AddClient_ValidData_ReturnsOkResult()
        {
            var request = new Domain.DTOs.Client();

            _clientModel.Setup(m => m.AddClient(It.IsAny<Domain.DTOs.Client>())).Returns(new Domain.DTOs.Client { });

            var result = _controller.AddClient(request);

            var okResult = Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void AddClient_ReturnNotFound()
        {
            var request = new Domain.DTOs.Client();

            _clientModel.Setup(m => m.AddClient(It.IsAny<Domain.DTOs.Client>())).Returns((Domain.DTOs.Client)null);

            var result = _controller.AddClient(request);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void AddClient_ReturnBadRequest()
        {
            var request = new Domain.DTOs.Client
            {
                FirstName = "Magazine Name",
                LastName = " Name"
            };

            _clientModel.Setup(model => model.AddClient(request)).Throws(new ArgumentException());

            var result = _controller.AddClient(request);

            Assert.IsType<BadRequestObjectResult>(result);
        }
       
        [Fact]
        public void GetClientById_ReturnOK()
        {

            var clientId = 1;
            var expectedClient = new Domain.DTOs.Client
            {
                ClientID = clientId,
                FirstName = "20/2023"
            };

            _clientModel.Setup(model => model.GetClientById(clientId)).Returns(expectedClient);


            var result = _controller.GetClientById(clientId);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void GetClientById_ReturnNotFound()
        {
            var clientId = 1;
            _clientModel.Setup(model => model.GetClientById(clientId)).Returns(null as Domain.DTOs.Client);

            var result = _controller.GetClientById(clientId);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void GetClientById_ReturnbBadRequest()
        {
            var clientId = 1;
            _clientModel.Setup(model => model.GetClientById(clientId)).Throws(new Exception("Error retrieving book"));

            var result = _controller.GetClientById(clientId);

            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void UpdateClient_ReturnOK()
        {
            int Id = 123;
            var request = new Domain.DTOs.Client();

            _clientModel.Setup(m => m.UpdateClient(It.IsAny<int>(), It.IsAny<Domain.DTOs.Client>())).Returns(new Domain.DTOs.Client { });

            var result = _controller.UpdateClient(Id, request);

            var okResult = Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void UpdateClient_ReturnNotFound()
        {
            int clientId = 123;
            var request = new Domain.DTOs.Client();

            _clientModel.Setup(m => m.UpdateClient(It.IsAny<int>(), It.IsAny<Domain.DTOs.Client>())).Returns((Domain.DTOs.Client)null);

            var result = _controller.UpdateClient(clientId, request);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void UpdateClient_ReturnbBadRequest()
        {
            var clientId = 1;
            var request = new Domain.DTOs.Client
            {
                FirstName = "Updated Magazine Name"
            };

            _clientModel.Setup(model => model.UpdateClient(clientId, request)).Throws(new Exception("Update failed"));

            var result = _controller.UpdateClient(clientId, request);

            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void DeleteClient_ReturnNoContent()
        {
            var clientId = 1;
            _clientModel.Setup(model => model.GetClientById(clientId)).Returns(new Domain.DTOs.Client());

            var result = _controller.DeleteClient(clientId);

            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public void DeleteClient_ReturnNotFound()
        {
            var clientId = 1;
            _clientModel.Setup(model => model.GetClientById(clientId)).Returns((Domain.DTOs.Client)null);

            var result = _controller.DeleteClient(clientId);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void DeleteClient_ReturnsBadRequest_WhenExceptionThrown()
        {
            var clientId = 1;
            _clientModel.Setup(model => model.GetClientById(clientId)).Throws<Exception>();

            var result = _controller.DeleteClient(clientId);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
