using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Repository;

namespace VirtualLibraryAPI.Tests
{
    public class ClientModelTest
    {
        private readonly ILogger<Models.ClientModel> _logger;
        private readonly Mock<IClientRepository> _clientRepository;

        public ClientModelTest()
        {
            _clientRepository = new Mock<IClientRepository>();
            _logger = new Mock<ILogger<Models.ClientModel>>().Object;
        }

        [Fact]
        public void AddClient_ReturnsAddedClient()
        {
            var clientDto = new Domain.DTOs.Client { ClientID = 2, FirstName = "324235", LastName = "Test Author" };
            var addedClient = new Domain.DTOs.Client { ClientID = 1, FirstName = "324235", LastName = "Test Author" };
            _clientRepository.Setup(x => x.AddClient(clientDto)).Returns(addedClient);
            var clientModel = new Models.ClientModel(_logger, _clientRepository.Object);

            var result = clientModel.AddClient(clientDto);

            Assert.NotNull(result);
            Assert.Equal(clientDto.FirstName, result.FirstName);
            Assert.Equal(clientDto.LastName, result.LastName);
            _clientRepository.Verify(x => x.AddClient(clientDto), Times.Once());
        }
        [Fact]
        public void UpdateClient_Should_Return_Updated_Client()
        {
            var clientID = 1;
            var clientDto = new Domain.DTOs.Client { ClientID = 2, FirstName = "324235", LastName = "Test Author" };
            var updatedClient = new Domain.DTOs.Client { ClientID = clientID, FirstName = "324235", LastName = "Test Author" };
            _clientRepository.Setup(x => x.UpdateClient(clientID, clientDto)).Returns(updatedClient);
            var clientModel = new Models.ClientModel(_logger, _clientRepository.Object);

            var result = clientModel.UpdateClient(clientID, clientDto);

            Assert.True(result.ClientID == 1);
            Assert.Equal(updatedClient.ClientID, result.ClientID);
            Assert.Equal(updatedClient.FirstName, result.FirstName);
            Assert.Equal(updatedClient.LastName, result.LastName);
            _clientRepository.Verify(x => x.UpdateClient(clientID, clientDto), Times.Once());
        }
        [Fact]
        public void DeleteClient_ReturnsDeletedClient()
        {
            int clientIdToDelete = 1;
            var expectedDeletedClient = new Domain.DTOs.Client { ClientID = clientIdToDelete, FirstName = "1234567890", LastName = "Test Author" };
            _clientRepository.Setup(x => x.DeleteClient(clientIdToDelete)).Returns(expectedDeletedClient);
            var clientModel = new Models.ClientModel(_logger, _clientRepository.Object);

            var deletedClient = clientModel.DeleteClient(clientIdToDelete);

            _clientRepository.Verify(x => x.DeleteClient(clientIdToDelete), Times.Once());
            Assert.Equal(expectedDeletedClient, deletedClient);
        }
        [Fact]
        public void GetAllClients_ReturnsAllClients()
        {
            var expectedClients = new List<Domain.DTOs.Client>
        {
            new Domain.DTOs.Client { ClientID = 1, FirstName = "1234567890", LastName = "Test Author 1" },
            new Domain.DTOs.Client { ClientID = 2, FirstName = "0987654321", LastName = "Test Author 2" },
            new Domain.DTOs.Client { ClientID = 3, FirstName = "5555555555", LastName = "Test Author 3" }
        };
            _clientRepository.Setup(x => x.GetAllClients()).Returns(expectedClients);
            var clientModel = new Models.ClientModel(_logger, _clientRepository.Object);

            var allClients = clientModel.GetAllClients();

            _clientRepository.Verify(x => x.GetAllClients(), Times.Once());
            Assert.Equal(expectedClients, allClients);
        }
        [Fact]
        public void GetClientById_ReturnsCorrectClient()
        {
            var clientId = 1;
            var expectedClient = new Domain.DTOs.Client { ClientID = clientId, FirstName = "1234567890", LastName = "Test Author" };
            _clientRepository.Setup(x => x.GetClientById(clientId)).Returns(expectedClient);
            var clientModel = new Models.ClientModel(_logger, _clientRepository.Object);

            var client = clientModel.GetClientById(clientId);

            _clientRepository.Verify(x => x.GetClientById(clientId), Times.Once());
            Assert.Equal(expectedClient, client);
        }
        [Fact]
        public void GetClientByIdResponse_ReturnsCorrectResponseDTO()
        {

            var clientId = 1;
            var expectedClientDTO = new Domain.DTOs.Client { ClientID = clientId, FirstName = "1234567890", LastName = "Test Author" };
            _clientRepository.Setup(x => x.GetClientByIdResponse(clientId)).Returns(expectedClientDTO);
            var clientModel = new Models.ClientModel(_logger, _clientRepository.Object);


            var bookDTO = clientModel.GetClientByIdResponse(clientId);

            _clientRepository.Verify(x => x.GetClientByIdResponse(clientId), Times.Once());
            Assert.Equal(expectedClientDTO, bookDTO);
        }
        [Fact]
        public void GetAllClientsResponse_ReturnsExpectedResult()
        {
            var clientModel = new Models.ClientModel(_logger, _clientRepository.Object);
            var expectedClients = new List<Domain.DTOs.Client>
            {
                new Domain.DTOs.Client  { ClientID = 1, FirstName = "1234567890", LastName = "Author 1" },
                new Domain.DTOs.Client  { ClientID = 2, FirstName = "0987654321", LastName = "Author 2" },
                new Domain.DTOs.Client  { ClientID = 3, FirstName = "1111111111", LastName = "Author 3" }
            };

            _clientRepository.Setup(repo => repo.GetAllClientsResponse())
                .Returns(expectedClients);

            var result = clientModel.GetAllClientsResponse();

            Assert.Equal(expectedClients, result);
            _clientRepository.Verify(x => x.GetAllClientsResponse(), Times.Once);
        }
       
    }
}
