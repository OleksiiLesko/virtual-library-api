﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Domain;

namespace VirtualLibraryAPI.Repository.Repositories
{
    /// <summary>
    /// User repository 
    /// </summary>
    public class User : IUserRepository
    {
        /// <summary>
        /// Application context
        /// </summary>
        private readonly ApplicationContext _context;
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<User> _logger;
        /// <summary>
        /// Constructor with context and logger
        /// </summary>
        /// <param name="context"></param>
        public User(ApplicationContext context, ILogger<User> logger)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// Add user to the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Domain.DTOs.User AddUser(Domain.DTOs.User user)
        {
            var newUser = new Domain.Entities.User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            _logger.LogInformation("Adding user to the database: {UserID}", newUser.UserID);

            var addedUser = new Domain.DTOs.User
            {
                UserID = newUser.UserID,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
            };

            return addedUser;
        }
        /// <summary>
        /// Delete user from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.DTOs.User DeleteUser(int id)
        {
            var user = _context.Users.Find(id);

            _context.Users.Remove(user);
            _context.SaveChanges();
            var deletedUserDto = new Domain.DTOs.User
            {
                UserID = user.UserID,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };
            _logger.LogInformation("Deleting user from database: {ArticleID}", user.UserID);

            return deletedUserDto;
        }
        /// <summary>
        /// Get all users from database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<Domain.DTOs.User> GetAllUsers()
        {
            var userEntities = _context.Users.ToList();
            var userDtos = new List<Domain.DTOs.User>();

            foreach (var userEntity in userEntities)
            {
                var userDto = new Domain.DTOs.User();

                userDtos.Add(userDto);
            }
            _logger.LogInformation("Returning all users from the database");


            return userDtos;
        }
        /// <summary>
        /// Get user by id from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.DTOs.User GetUserById(int id)
        {
            _logger.LogInformation($"Getting article id: ArticleID {id}");

            var userEntity = _context.Users.FirstOrDefault(b => b.UserID == id);
            if (userEntity == null)
            {
                return null;
            }
            var userDto = new Domain.DTOs.User
            {
                UserID = userEntity.UserID,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
            };

            return userDto;
        }
        /// <summary>
        /// Get all users for response
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Domain.DTOs.User> GetAllUsersResponse()
        {
            _logger.LogInformation("Get all users for response DTO:");

            var users = _context.Users
                .Select(x => new Domain.DTOs.User
                {
                    UserID = x.UserID,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                }).ToList();
            return users;
        }
        /// <summary>
        /// Get user by id for response
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>

        public Domain.DTOs.User GetUserByIdResponse(int id)
        {
            var result = _context.Users
             .FirstOrDefault(x => x.UserID == id);

            _logger.LogInformation($"Get user by id for response: UserID {id}");

            var userDTO = new Domain.DTOs.User
            {
                UserID = result.UserID,
                FirstName = result.FirstName,
                LastName = result.LastName,
            };
            return userDTO;
        }
        /// <summary>
        /// Update user data 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.DTOs.User UpdateUser(int id, Domain.DTOs.User user)
        {
            var existingUser = _context.Users.Find(id);

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;

            _context.SaveChanges();
            _logger.LogInformation("Update user by id in the database: {UserID}", existingUser.UserID);

            return user;
        }
    }
}
