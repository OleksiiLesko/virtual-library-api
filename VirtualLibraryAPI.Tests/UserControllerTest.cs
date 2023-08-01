using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using VirtualLibraryAPI.Common;
using VirtualLibraryAPI.Library.Controllers;
using VirtualLibraryAPI.Models;

namespace VirtualLibraryAPI.Tests
{
    public class UserControllerTest
    {
        private readonly Mock<ILogger<UserController>> _logger;
        private readonly Mock<IUserModel> _userModel;
        private readonly Mock<IDepartmentModel> _departmentModel;
        private readonly UserController _controller;

        public UserControllerTest()
        {
            _logger = new Mock<ILogger<UserController>>();
            _userModel = new Mock<IUserModel>();
            _departmentModel = new Mock<IDepartmentModel>();
            _controller = new UserController(_logger.Object, _userModel.Object, _departmentModel.Object);
        }

        [Fact]
        public void GetAllUsers_ReturnsOk()
        {
            var users = new List<Domain.DTOs.User>(); 
            _userModel.Setup(m => m.GetAllUsers()).Returns(users);

            var result = _controller.GetAllUsers();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetAllUsers_ReturnsNotFound()
        {
            List<Domain.DTOs.User> users = null;
            _userModel.Setup(m => m.GetAllUsers()).Returns(users);

            var result = _controller.GetAllUsers();

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetAllUsers_ReturnsBadRequest()
        {
            _userModel.Setup(m => m.GetAllUsers()).Throws(new Exception());

            var result = _controller.GetAllUsers();

            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed", badRequestResult.Value);
        }
        [Fact]
        public void AddUser_ValidData_ReturnsOkResult()
        {
            var request = new Domain.DTOs.User();
            var userType = UserType.Administrator;

            _departmentModel.Setup(m => m.GetDepartmentById(It.IsAny<int>())).Returns(new Domain.DTOs.Department { });

            _userModel.Setup(m => m.AddUser(It.IsAny<Domain.DTOs.User>(), It.IsAny<UserType>())).Returns(new Domain.DTOs.User { });

            var result = _controller.AddUser(123, request, userType);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var userResponse = Assert.IsType<Domain.DTOs.User>(okResult.Value);
            Assert.Equal(userType, userResponse.UserType);
        }
        [Fact]
        public void AddUser_UserModelReturnsNull_ReturnsNotFound()
        {
            var request = new Domain.DTOs.User();
            var userType = UserType.Administrator;

            _departmentModel.Setup(m => m.GetDepartmentById(It.IsAny<int>())).Returns(new Domain.DTOs.Department { });

            _userModel.Setup(m => m.AddUser(It.IsAny<Domain.DTOs.User>(), It.IsAny<UserType>())).Returns((Domain.DTOs.User)null);

            var result = _controller.AddUser(123, request, userType);

            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public void AddUser_InvalidDepartment_ReturnsBadRequest()
        {
            var request = new Domain.DTOs.User
            {
                DepartmentID = 999 
            };
            var userType = UserType.Administrator;

            _departmentModel.Setup(m => m.GetDepartmentById(It.IsAny<int>())).Returns((Domain.DTOs.Department)null);

            var result = _controller.AddUser(123, request, userType);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid DepartmentID. Department with the specified ID does not exist.", badRequestResult.Value);
        }
        [Fact]
        public void GetUserById_ReturnsOk()
        {
            int userId = 1; 
            var user = new Domain.DTOs.User(); 
            _userModel.Setup(m => m.GetUserById(userId)).Returns(user);

            var result = _controller.GetUserById(userId);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetUserById_ReturnsNotFound()
        {
            int userId = 1; 
            Domain.DTOs.User user = null;
            _userModel.Setup(m => m.GetUserById(userId)).Returns(user);

            var result = _controller.GetUserById(userId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetUserById_ReturnsBadRequest()
        {
            int userId = 1; 
            _userModel.Setup(m => m.GetUserById(userId)).Throws(new Exception("Failed to retrieve user"));

            var result = _controller.GetUserById(userId);

            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed", badRequestResult.Value);
        }
        [Fact]
        public void UpdateUser_ValidData_ReturnsOkResult()
        {
            int userId = 123;
            var request = new Domain.DTOs.User();
            var userType = UserType.Administrator;

            _departmentModel.Setup(m => m.GetDepartmentById(It.IsAny<int>())).Returns(new Domain.DTOs.Department { });

            _userModel.Setup(m => m.UpdateUser(It.IsAny<int>(), It.IsAny<Domain.DTOs.User>(), It.IsAny<UserType>())).Returns(new Domain.DTOs.User { });

            var result = _controller.UpdateUser(userId, request, userType);

            var okResult = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void UpdateUser_UserModelReturnsNull_ReturnsNotFound()
        {
            int userId = 123;
            var request = new Domain.DTOs.User();
            var userType = UserType.Administrator;

            _departmentModel.Setup(m => m.GetDepartmentById(It.IsAny<int>())).Returns(new Domain.DTOs.Department { });

            _userModel.Setup(m => m.UpdateUser(It.IsAny<int>(), It.IsAny<Domain.DTOs.User>(), It.IsAny<UserType>())).Returns((Domain.DTOs.User)null);

            var result = _controller.UpdateUser(userId, request, userType);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void UpdateUser_InvalidDepartment_ReturnsBadRequest()
        {
            int userId = 123; 
            var request = new Domain.DTOs.User()
            {
                DepartmentID = 999 
            };
            var userType = UserType.Administrator;

            _departmentModel.Setup(m => m.GetDepartmentById(It.IsAny<int>())).Returns((Domain.DTOs.Department)null);

            var result = _controller.UpdateUser(userId, request, userType);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid DepartmentID. Department with the specified ID does not exist.", badRequestResult.Value);
        }

        [Fact]
        public void DeleteUser_ReturnsNoContent()
        {
            int userId = 1; 
            var user = new Domain.DTOs.User();
            _userModel.Setup(m => m.GetUserById(userId)).Returns(user);

            var result = _controller.DeleteUser(userId);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteUser_ReturnsNotFound()
        {
            int userId = 1; 
            Domain.DTOs.User user = null;
            _userModel.Setup(m => m.GetUserById(userId)).Returns(user);

            var result = _controller.DeleteUser(userId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteUser_ReturnsBadRequest()
        {
            int userId = 1; 
            var user = new Domain.DTOs.User(); 
            _userModel.Setup(m => m.GetUserById(userId)).Returns(user);
            _userModel.Setup(m => m.DeleteUser(userId)).Throws(new Exception("Failed to delete user"));

            var result = _controller.DeleteUser(userId);

            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed", badRequestResult.Value);
        }
    }
}
