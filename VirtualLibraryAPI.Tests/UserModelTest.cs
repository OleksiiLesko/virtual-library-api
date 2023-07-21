using Microsoft.Extensions.Logging;
using Moq;
using VirtualLibraryAPI.Common;
using VirtualLibraryAPI.Repository;

namespace VirtualLibraryAPI.Tests
{
    public class UserModelTest
    {
        private readonly ILogger<Models.UserModel> _logger;
        private readonly Mock<IUserRepository> _repository;

        public UserModelTest()
        {
            _repository = new Mock<IUserRepository>();
            _logger = new Mock<ILogger<Models.UserModel>>().Object;
        }

        [Fact]
        public void AddUser_ReturnsAddedUser()
        {
            var type = UserType.Client;
            var userDto = new Domain.DTOs.User { UserID = 2, FirstName = "40", LastName = "32" };
            var addedUser = new Domain.DTOs.User { UserID = 1, FirstName = "40", LastName = "32" };
            _repository.Setup(x => x.AddUser(userDto, type)).Returns(addedUser);
            var userModel = new Models.UserModel(_logger, _repository.Object);

            var result = userModel.AddUser(userDto, type);

            Assert.NotNull(result);
            Assert.Equal(userDto.LastName, result.LastName);
            Assert.Equal(userDto.FirstName, result.FirstName);
            Assert.NotEqual(userDto.UserID, result.UserID);
            _repository.Verify(x => x.AddUser(userDto, type), Times.Once());
        }
        //[Fact]
        //public void UpdateUser_Should_Return_Updated_User()
        //{
        //    var userID = 1;
        //    var userDto = new Domain.DTOs.User { UserID = 2, FirstName = "40", LastName = "32" };
        //    var updatedUser = new Domain.DTOs.User { UserID = 2, FirstName = "40", LastName = "32" };
        //    _repository.Setup(x => x.UpdateUser(userID, userDto)).Returns(updatedUser);
        //    var userModel = new Models.UserModel(_logger, _repository.Object);

        //    var result = userModel.UpdateUser(userID, userDto);

        //    Assert.Equal(updatedUser.UserID, result.UserID);
        //    Assert.Equal(updatedUser.FirstName, result.FirstName);
        //    _repository.Verify(x => x.UpdateUser(userID, userDto), Times.Once());
        //}
        [Fact]
        public void DeleteUser_ReturnsDeletedUser()
        {
            int userID = 1;
            var expectedDeletedUser = new Domain.DTOs.User { UserID = 2, FirstName = "40", LastName = "32" };
            _repository.Setup(x => x.DeleteUser(userID)).Returns(expectedDeletedUser);
            var userModel = new Models.UserModel(_logger, _repository.Object);

            var deletedUser = userModel.DeleteUser(userID);

            _repository.Verify(x => x.DeleteUser(userID), Times.Once());
            Assert.Equal(expectedDeletedUser, deletedUser);
        }
        [Fact]
        public void GetAllUsers_ReturnsAllUsers()
        {
            var expectedUsers = new List<Domain.DTOs.User>
        {
            new Domain.DTOs.User { UserID = 1, FirstName = "40", LastName = "32" },
            new Domain.DTOs.User { UserID = 2, FirstName = "40", LastName = "32" },
            new Domain.DTOs.User { UserID = 3, FirstName = "40", LastName = "32" }
        };
            _repository.Setup(x => x.GetAllUsers()).Returns(expectedUsers);
            var userModel = new Models.UserModel(_logger, _repository.Object);

            var allUsers = userModel.GetAllUsers();

            _repository.Verify(x => x.GetAllUsers(), Times.Once());
            Assert.Equal(expectedUsers, allUsers);
        }
        [Fact]
        public void GetUserById_ReturnsCorrectUser()
        {
            var userId = 1;
            var expectedUser = new Domain.DTOs.User { UserID = 1, FirstName = "40", LastName = "32" };
            _repository.Setup(x => x.GetUserById(userId)).Returns(expectedUser);
            var userModel = new Models.UserModel(_logger, _repository.Object);

            var user = userModel.GetUserById(userId);

            _repository.Verify(x => x.GetUserById(userId), Times.Once());
            Assert.Equal(expectedUser, user);
        }
        [Fact]
        public void GetUserByIdResponse_ReturnsCorrectResponseDTO()
        {

            var userId = 1;
            var expectedUserDTO = new Domain.DTOs.User { UserID = 1, FirstName = "40", LastName = "32" };
            _repository.Setup(x => x.GetUserByIdResponse(userId)).Returns(expectedUserDTO);
            var userModel = new Models.UserModel(_logger, _repository.Object);


            var userDTO = userModel.GetUserByIdResponse(userId);

            _repository.Verify(x => x.GetUserByIdResponse(userId), Times.Once());
            Assert.Equal(expectedUserDTO, userDTO);
        }
        [Fact]
        public void GetAllUsersResponse_ReturnsExpectedResult()
        {
            var userModel = new Models.UserModel(_logger, _repository.Object);
            var expectedUsers = new List<Domain.DTOs.User>
            {
                new Domain.DTOs.User { UserID = 1, FirstName = "40", LastName = "32" },
                new Domain.DTOs.User { UserID = 1, FirstName = "40", LastName = "32" },
                new Domain.DTOs.User { UserID = 1, FirstName = "40", LastName = "32" }
            };

            _repository.Setup(repo => repo.GetAllUsersResponse())
                .Returns(expectedUsers);

            var result = userModel.GetAllUsersResponse();

            Assert.Equal(expectedUsers, result);
            _repository.Verify(x => x.GetAllUsersResponse(), Times.Once);
        }
    }
}
