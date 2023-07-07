using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Common;
using VirtualLibraryAPI.Models;
using VirtualLibraryAPI.Repository;

namespace VirtualLibraryAPI.Tests
{
    public class ValidationUserModelTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<ILogger<ValidationUserModel>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;

        public ValidationUserModelTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _loggerMock = new Mock<ILogger<ValidationUserModel>>();
            _configurationMock = new Mock<IConfiguration>();
        }

        [Fact]
        public void CanUserReserveCopy_UserNotFound_ReturnsUserNotFoundStatus()
        {
            int userId = 1;
            _userRepositoryMock.Setup(repo => repo.GetUserById(userId)).Returns((Domain.DTOs.User)null);
            var validationUserModel = new ValidationUserModel(_loggerMock.Object, _userRepositoryMock.Object, _configurationMock.Object);

            var result = validationUserModel.CanUserReserveCopy(userId);

            Assert.Equal(ValidationUserStatus.UserNotFound, result);
        }
        [Fact]
        public void CanUserReserveCopy_MaxCopiesExceeded_ReturnsMaxCopiesExceededStatus()
        {
            int userId = 1;
            var user = new Domain.DTOs.User { UserID = userId };
            _userRepositoryMock.Setup(repo => repo.GetUserById(userId)).Returns(user);
            _userRepositoryMock.Setup(repo => repo.CountUserCopies(userId)).Returns(4); 
            _configurationMock.Setup(config => config["MaxCopies:Copy"]).Returns("3");
            var validationUserModel = new ValidationUserModel(_loggerMock.Object, _userRepositoryMock.Object, _configurationMock.Object);

            var result = validationUserModel.CanUserReserveCopy(userId);

            Assert.Equal(ValidationUserStatus.MaxCopiesExceeded, result);
        }
        [Fact]
        public void CanUserReserveCopy_HasExpiredCopy_ReturnsExpiredCopyStatus()
        {
            int userId = 1;
            var user = new Domain.DTOs.User { UserID = userId };
            _userRepositoryMock.Setup(repo => repo.GetUserById(userId)).Returns(user);
            _userRepositoryMock.Setup(repo => repo.CountUserCopies(userId)).Returns(2); 
            _userRepositoryMock.Setup(repo => repo.HasExpiredCopy(userId)).Returns(true); 
            _configurationMock.Setup(config => config["MaxCopies:Copy"]).Returns("5"); 
            var validationUserModel = new ValidationUserModel(_loggerMock.Object, _userRepositoryMock.Object, _configurationMock.Object);

            var result = validationUserModel.CanUserReserveCopy(userId);

            Assert.Equal(ValidationUserStatus.ExpiredCopy, result);
        }
        [Fact]
        public void CanUserReserveCopy_ValidUser_ReturnsValidStatus()
        {
            int userId = 1;
            var user = new Domain.DTOs.User { UserID = userId };
            _userRepositoryMock.Setup(repo => repo.GetUserById(userId)).Returns(user);
            _userRepositoryMock.Setup(repo => repo.CountUserCopies(userId)).Returns(2); 
            _userRepositoryMock.Setup(repo => repo.HasExpiredCopy(userId)).Returns(false); 
            _configurationMock.Setup(config => config["MaxCopies:Copy"]).Returns("5"); 
            var validationUserModel = new ValidationUserModel(_loggerMock.Object, _userRepositoryMock.Object, _configurationMock.Object);

            var result = validationUserModel.CanUserReserveCopy(userId);

            Assert.Equal(ValidationUserStatus.Valid, result);
        }
        [Fact]
        public void CanUserReserveCopy_DbException_ReturnsDbErrorStatus()
        {
            int userId = 1;
            var dbExceptionMock = new Mock<DbException>();
            _userRepositoryMock.Setup(repo => repo.GetUserById(userId)).Throws(dbExceptionMock.Object);
            var validationUserModel = new ValidationUserModel(_loggerMock.Object, _userRepositoryMock.Object, _configurationMock.Object);


            var result = validationUserModel.CanUserReserveCopy(userId);

            Assert.Equal(ValidationUserStatus.DbError, result);
        }

        [Fact]
        public void CanUserReserveCopy_GenericException_ReturnsInternalServerErrorStatus()
        {
            int userId = 1;
            _userRepositoryMock.Setup(repo => repo.GetUserById(userId)).Throws<Exception>();
            var validationUserModel = new ValidationUserModel(_loggerMock.Object, _userRepositoryMock.Object, _configurationMock.Object);

            var result = validationUserModel.CanUserReserveCopy(userId);

            Assert.Equal(ValidationUserStatus.InternalServerError, result);
        }
    }
}
