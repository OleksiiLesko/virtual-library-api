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
    public class ValidationIssuerModelTest
    {
        [Fact]
        public void CanIssuerIssueCopy_UserNotFound_ReturnsUserNotFoundStatus()
        {
            var loggerMock = new Mock<ILogger<ValidationIssuerModel>>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var copyRepositoryMock = new Mock<ICopyRepository>();

            userRepositoryMock.Setup(repo => repo.GetUserById(It.IsAny<int>())).Returns((Domain.DTOs.User)null);

            var model = new ValidationIssuerModel(loggerMock.Object, userRepositoryMock.Object, copyRepositoryMock.Object);

            var result = model.CanIssuerIssueCopy(123, 456);

            Assert.Equal(ValidationIssuerStatus.UserNotFound, result);
        }

        [Fact]
        public void CanIssuerIssueCopy_CopyNotFound_ReturnsCopyNotFoundStatus()
        {
            var loggerMock = new Mock<ILogger<ValidationIssuerModel>>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var copyRepositoryMock = new Mock<ICopyRepository>();
            Domain.DTOs.Copy copies = null;
            userRepositoryMock.Setup(repo => repo.GetUserById(It.IsAny<int>())).Returns(new Domain.DTOs.User());
            copyRepositoryMock.Setup(repo => repo.GetCopyById(It.IsAny<int>())).Returns(copies);

            var model = new ValidationIssuerModel(loggerMock.Object, userRepositoryMock.Object, copyRepositoryMock.Object);

            var result = model.CanIssuerIssueCopy(123, 456);

            Assert.Equal(ValidationIssuerStatus.CopyNotFound, result);
        }

        [Fact]
        public void CanIssuerIssueCopy_DbException_ReturnsDbErrorStatus()
        {
            var loggerMock = new Mock<ILogger<ValidationIssuerModel>>();
            var dbExceptionMock = new Mock<DbException>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var copyRepositoryMock = new Mock<ICopyRepository>();

            userRepositoryMock.Setup(repo => repo.GetUserById(It.IsAny<int>())).Throws(dbExceptionMock.Object);

            var model = new ValidationIssuerModel(loggerMock.Object, userRepositoryMock.Object, copyRepositoryMock.Object);

            var result = model.CanIssuerIssueCopy(123, 456);

            Assert.Equal(ValidationIssuerStatus.DbError, result);
        }

        [Fact]
        public void CanIssuerIssueCopy_InternalServerError_ReturnsInternalServerErrorStatus()
        {
            var loggerMock = new Mock<ILogger<ValidationIssuerModel>>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var copyRepositoryMock = new Mock<ICopyRepository>();

            userRepositoryMock.Setup(repo => repo.GetUserById(It.IsAny<int>())).Throws(new System.Exception());

            var model = new ValidationIssuerModel(loggerMock.Object, userRepositoryMock.Object, copyRepositoryMock.Object);

            var result = model.CanIssuerIssueCopy(123, 456);

            Assert.Equal(ValidationIssuerStatus.InternalServerError, result);
        }
        [Fact]
        public void CanIssuerIssueCopy_UserTypeManager_ReturnsValidStatus()
        {
            var loggerMock = new Mock<ILogger<ValidationIssuerModel>>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var copyRepositoryMock = new Mock<ICopyRepository>();

            userRepositoryMock.Setup(repo => repo.GetUserById(It.IsAny<int>())).Returns(new Domain.DTOs.User
            {
                UserType = UserType.Manager
            });
            copyRepositoryMock.Setup(repo => repo.GetCopyById(It.IsAny<int>())).Returns(new Domain.DTOs.Copy());

            var model = new ValidationIssuerModel(loggerMock.Object, userRepositoryMock.Object, copyRepositoryMock.Object);

            var result = model.CanIssuerIssueCopy(123, 456);

            Assert.Equal(ValidationIssuerStatus.Valid, result);
        }

        [Fact]
        public void CanIssuerIssueCopy_UserTypeAdministrator_UserDepartmentEqualCopyDepartment_ReturnsValidStatus()
        {
            var loggerMock = new Mock<ILogger<ValidationIssuerModel>>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var copyRepositoryMock = new Mock<ICopyRepository>();

            userRepositoryMock.Setup(repo => repo.GetUserById(It.IsAny<int>())).Returns(new Domain.DTOs.User
            {
                UserType = UserType.Administrator,
                DepartmentID = 123 
            });
            copyRepositoryMock.Setup(repo => repo.GetCopyById(It.IsAny<int>())).Returns(new Domain.DTOs.Copy
            {
                DepartmentID = 123 
            });

            var model = new ValidationIssuerModel(loggerMock.Object, userRepositoryMock.Object, copyRepositoryMock.Object);

            var result = model.CanIssuerIssueCopy(123, 456);

            Assert.Equal(ValidationIssuerStatus.Valid, result);
        }

        [Fact]
        public void CanIssuerIssueCopy_UserTypeAdministrator_UserDepartmentNotEqualCopyDepartment_ReturnsUserDepartmentNotEqualCopyDepartmentStatus()
        {
            var loggerMock = new Mock<ILogger<ValidationIssuerModel>>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var copyRepositoryMock = new Mock<ICopyRepository>();

            userRepositoryMock.Setup(repo => repo.GetUserById(It.IsAny<int>())).Returns(new Domain.DTOs.User
            {
                UserType = UserType.Administrator,
                DepartmentID = 123 
            });
            copyRepositoryMock.Setup(repo => repo.GetCopyById(It.IsAny<int>())).Returns(new Domain.DTOs.Copy
            {
                DepartmentID = 456 
            });

            var model = new ValidationIssuerModel(loggerMock.Object, userRepositoryMock.Object, copyRepositoryMock.Object);

            var result = model.CanIssuerIssueCopy(123, 456);

            Assert.Equal(ValidationIssuerStatus.UserDepartmentNotEqualCopyDepartment, result);
        }
    }
}
