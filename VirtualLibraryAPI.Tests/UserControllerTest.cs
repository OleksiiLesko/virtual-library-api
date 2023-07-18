using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Common;
using VirtualLibraryAPI.Library.Controllers;
using VirtualLibraryAPI.Models;
using VirtualLibraryAPI.Repository;

namespace VirtualLibraryAPI.Tests
{
    public class UserControllerTest
    {
        private readonly Mock<ILogger<UserController>> _logger;
        private readonly Mock<IUserModel> _model;
        private readonly UserController _controller;

        public UserControllerTest()
        {
            _logger = new Mock<ILogger<UserController>>();
            _model = new Mock<IUserModel>();
            _controller = new UserController(_logger.Object, _model.Object);
        }

        [Fact]
        public void GetAllUsers_ReturnsOk()
        {
            var users = new List<Domain.DTOs.User>(); 
            _model.Setup(m => m.GetAllUsers()).Returns(users);

            var result = _controller.GetAllUsers();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetAllUsers_ReturnsNotFound()
        {
            List<Domain.DTOs.User> users = null;
            _model.Setup(m => m.GetAllUsers()).Returns(users);

            var result = _controller.GetAllUsers();

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetAllUsers_ReturnsBadRequest()
        {
            _model.Setup(m => m.GetAllUsers()).Throws(new Exception());

            var result = _controller.GetAllUsers();

            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed", badRequestResult.Value);
        }
        [Fact]
        public void AddUser_ReturnsOk()
        {
            var userId = 1;
            var type = UserType.Client;
            var userRequest = new Domain.DTOs.User();
            var addedUser = new Domain.DTOs.User();
            _model.Setup(m => m.AddUser(userRequest, type)).Returns(addedUser);

            var result = _controller.AddUser(userId,userRequest, type);

            Assert.IsType<OkObjectResult>(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var returnedUser = Assert.IsType<Domain.DTOs.User>(okObjectResult.Value);
            Assert.Equal(addedUser.UserID, returnedUser.UserID);
            Assert.Equal(addedUser.FirstName, returnedUser.FirstName);
            Assert.Equal(addedUser.LastName, returnedUser.LastName);
        }

        [Fact]
        public void AddUser_ReturnsNotFound()
        {
            var userId = 1;
            var type = UserType.Client;
            var userRequest = new Domain.DTOs.User();
            Domain.DTOs.User addedUser = null;
            _model.Setup(m => m.AddUser(userRequest,type)).Returns(addedUser);

            var result = _controller.AddUser(userId,userRequest, type);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void AddUser_ReturnsBadRequest()
        {
            var userId = 1;
            var type = UserType.Client;
            var userRequest = new Domain.DTOs.User();
            _model.Setup(m => m.AddUser(userRequest, type)).Throws(new Exception("Failed to add user"));

            var result = _controller.AddUser(userId,userRequest, type);

            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed", badRequestResult.Value);
        }
        [Fact]
        public void GetUserById_ReturnsOk()
        {
            int userId = 1; 
            var user = new Domain.DTOs.User(); 
            _model.Setup(m => m.GetUserById(userId)).Returns(user);

            var result = _controller.GetUserById(userId);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetUserById_ReturnsNotFound()
        {
            int userId = 1; 
            Domain.DTOs.User user = null;
            _model.Setup(m => m.GetUserById(userId)).Returns(user);

            var result = _controller.GetUserById(userId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetUserById_ReturnsBadRequest()
        {
            int userId = 1; 
            _model.Setup(m => m.GetUserById(userId)).Throws(new Exception("Failed to retrieve user"));

            var result = _controller.GetUserById(userId);

            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed", badRequestResult.Value);
        }
        [Fact]
        public void UpdateUser_ReturnsOk()
        {
            int userId = 1; 
            var userRequest = new Domain.DTOs.User(); 
            var updatedUser = new Domain.DTOs.User(); 
            _model.Setup(m => m.UpdateUser(userId, userRequest)).Returns(updatedUser);

            var result = _controller.UpdateUser(userId, userRequest);

            Assert.IsType<OkObjectResult>(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var returnedUser = Assert.IsType<Domain.DTOs.User>(okObjectResult.Value);
            Assert.Equal(updatedUser.UserID, returnedUser.UserID);
            Assert.Equal(userRequest.FirstName, returnedUser.FirstName);
            Assert.Equal(userRequest.LastName, returnedUser.LastName);
        }

        [Fact]
        public void UpdateUser_ReturnsNotFound()
        {
            int userId = 1;
            var userRequest = new Domain.DTOs.User(); 
            Domain.DTOs.User updatedUser = null;
            _model.Setup(m => m.UpdateUser(userId, userRequest)).Returns(updatedUser);

            var result = _controller.UpdateUser(userId, userRequest);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void UpdateUser_ReturnsBadRequest()
        {
            int userId = 1;
            var userRequest = new Domain.DTOs.User(); 
            _model.Setup(m => m.UpdateUser(userId, userRequest)).Throws(new Exception("Failed to update user"));

            var result = _controller.UpdateUser(userId, userRequest);

            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed", badRequestResult.Value);
        }
        [Fact]
        public void DeleteUser_ReturnsNoContent()
        {
            int userId = 1; 
            var user = new Domain.DTOs.User();
            _model.Setup(m => m.GetUserById(userId)).Returns(user);

            var result = _controller.DeleteUser(userId);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteUser_ReturnsNotFound()
        {
            int userId = 1; 
            Domain.DTOs.User user = null;
            _model.Setup(m => m.GetUserById(userId)).Returns(user);

            var result = _controller.DeleteUser(userId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteUser_ReturnsBadRequest()
        {
            int userId = 1; 
            var user = new Domain.DTOs.User(); 
            _model.Setup(m => m.GetUserById(userId)).Returns(user);
            _model.Setup(m => m.DeleteUser(userId)).Throws(new Exception("Failed to delete user"));

            var result = _controller.DeleteUser(userId);

            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed", badRequestResult.Value);
        }
    }
}
