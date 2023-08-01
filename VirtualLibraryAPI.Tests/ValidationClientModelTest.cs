using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System.Data.Common;
using VirtualLibraryAPI.Common;
using VirtualLibraryAPI.Models;
using VirtualLibraryAPI.Repository;

namespace VirtualLibraryAPI.Tests
{
    public class ValidationClientModelTest
    {
        private readonly Mock<IClientRepository> _clientRepositoryMock;
        private readonly Mock<ILogger<ValidationClientModel>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;

        public ValidationClientModelTest()
        {
            _clientRepositoryMock = new Mock<IClientRepository>();
            _loggerMock = new Mock<ILogger<ValidationClientModel>>();
            _configurationMock = new Mock<IConfiguration>();
        }

        [Fact]
        public void CanClientReserveCopy_ClientNotFound_ReturnsClientNotFoundStatus()
        {
            int clientId = 1;
            _clientRepositoryMock.Setup(repo => repo.GetClientById(clientId)).Returns((Domain.DTOs.Client)null);
            var validationUserModel = new ValidationClientModel(_loggerMock.Object, _clientRepositoryMock.Object, _configurationMock.Object);

            var result = validationUserModel.CanClientReserveCopy(clientId);

            Assert.Equal(ValidationUserStatus.UserNotFound, result);
        }
        [Fact]
        public void CanClientReserveCopy_MaxCopiesExceeded_ReturnsMaxCopiesExceededStatus()
        {
            int clientID = 1;
            var client = new Domain.DTOs.Client { ClientID = clientID };
            _clientRepositoryMock.Setup(repo => repo.GetClientById(clientID)).Returns(client);
            _clientRepositoryMock.Setup(repo => repo.CountClientCopies(clientID)).Returns(4);
            _configurationMock.Setup(config => config["MaxCopies:Copy"]).Returns("3");
            var validationClientModel = new ValidationClientModel(_loggerMock.Object, _clientRepositoryMock.Object, _configurationMock.Object);

            var result = validationClientModel.CanClientReserveCopy(clientID);

            Assert.Equal(ValidationUserStatus.MaxCopiesExceeded, result);
        }
        [Fact]
        public void CanClientReserveCopy_HasExpiredCopy_ReturnsExpiredCopyStatus()
        {
            int clientId = 1;
            var client = new Domain.DTOs.Client { ClientID = clientId };
            _clientRepositoryMock.Setup(repo => repo.GetClientById(clientId)).Returns(client);
            _clientRepositoryMock.Setup(repo => repo.CountClientCopies(clientId)).Returns(2);
            _clientRepositoryMock.Setup(repo => repo.HasExpiredCopy(clientId)).Returns(true);
            _configurationMock.Setup(config => config["MaxCopies:Copy"]).Returns("5");
            var validationUserModel = new ValidationClientModel(_loggerMock.Object, _clientRepositoryMock.Object, _configurationMock.Object);

            var result = validationUserModel.CanClientReserveCopy(clientId);

            Assert.Equal(ValidationUserStatus.ExpiredCopy, result);
        }
        [Fact]
        public void CanClientReserveCopy_ValidClient_ReturnsValidStatus()
        {
            int clientId = 1;
            var client = new Domain.DTOs.Client { ClientID = clientId };
            _clientRepositoryMock.Setup(repo => repo.GetClientById(clientId)).Returns(client);
            _clientRepositoryMock.Setup(repo => repo.CountClientCopies(clientId)).Returns(2);
            _clientRepositoryMock.Setup(repo => repo.HasExpiredCopy(clientId)).Returns(false);
            _configurationMock.Setup(config => config["MaxCopies:Copy"]).Returns("5");
            var validationUserModel = new ValidationClientModel(_loggerMock.Object, _clientRepositoryMock.Object, _configurationMock.Object);

            var result = validationUserModel.CanClientReserveCopy(clientId);

            Assert.Equal(ValidationUserStatus.Valid, result);
        }
        [Fact]
        public void CanClientReserveCopy_DbException_ReturnsDbErrorStatus()
        {
            int clientId = 1;
            var dbExceptionMock = new Mock<DbException>();
            _clientRepositoryMock.Setup(repo => repo.GetClientById(clientId)).Throws(dbExceptionMock.Object);
            var validationUserModel = new ValidationClientModel(_loggerMock.Object, _clientRepositoryMock.Object, _configurationMock.Object);


            var result = validationUserModel.CanClientReserveCopy(clientId);

            Assert.Equal(ValidationUserStatus.DbError, result);
        }

        [Fact]
        public void CanUserReserveCopy_GenericException_ReturnsInternalServerErrorStatus()
        {
            int userId = 1;
            _clientRepositoryMock.Setup(repo => repo.GetClientById(userId)).Throws<Exception>();
            var validationUserModel = new ValidationClientModel(_loggerMock.Object, _clientRepositoryMock.Object, _configurationMock.Object);

            var result = validationUserModel.CanClientReserveCopy(userId);

            Assert.Equal(ValidationUserStatus.InternalServerError, result);
        }
    }
}
