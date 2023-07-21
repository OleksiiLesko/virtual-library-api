using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Common;
using VirtualLibraryAPI.Domain.DTOs;
using VirtualLibraryAPI.Repository;

namespace VirtualLibraryAPI.Models
{
    /// <summary>
    /// User model
    /// </summary>
    public class UserModel : IUserModel
    {
        /// <summary>
        /// Using article interface
        /// </summary>
        private readonly IUserRepository _repository;
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<UserModel> _logger;

        /// <summary>
        /// Constructor with article Repository and logger
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        public UserModel(ILogger<UserModel> logger, IUserRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        /// <summary>
        /// Add user from model
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User AddUser(User user, UserType userType, DepartmentType departmentType)
        {
            _logger.LogInformation($"Adding user from User model {user}");
            var result = _repository.AddUser(user, userType, departmentType);
            if (result == null)
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// Delete user from model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public User DeleteUser(int id)
        {
            _logger.LogInformation($"Deleting user from Article model: ArticleID {id}");
            var result = _repository.DeleteUser(id);
            if (result == null)
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// get all users from model
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<User> GetAllUsers()
        {
            _logger.LogInformation($"Getting all users from Article model ");
            var books = _repository.GetAllUsers();
            if (books.Any())
            {
                return books;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Get all users for response
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetAllUsersResponse()
        {
            _logger.LogInformation("Get all users for response DTO from user model  ");
            var result = _repository.GetAllUsersResponse();
            if (result == null)
            {
                return result;
            }
            return result;
        }

        /// <summary>
        /// Get users by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public User GetUserById(int id)
        {
            _logger.LogInformation($"Getting user by id from Article model: ArticleID {id} ");
            var result = _repository.GetUserById(id);
            if (result == null)
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// Get user by id for response
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public User GetUserByIdResponse(int id)
        {
            _logger.LogInformation($"Get user by id for response DTO from User model: UserID {id}");
            var result = _repository.GetUserByIdResponse(id);
            if (result == null)
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// Update user data by model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public User UpdateUser(int id, User user,UserType userType, DepartmentType departmentType)
        {
            _logger.LogInformation($"Updating user from User model: UserID {id}");
            var result = _repository.UpdateUser(id, user, userType, departmentType);
            if (result == null)
            {
                return result;
            }
            return result;
        }
    }
}
