using Microsoft.Extensions.Logging;
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
            _logger.LogInformation("Adding user to the database: {ArticleID}", newUser.UserID);

            var addedUser = new Domain.DTOs.User
            {
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
            var userDto = new Domain.DTOs.Article
            {
                ArticleID = userEntity.ItemID,
                CopyID = null,
                Name = itemEntity.Name,
                PublishingDate = itemEntity.PublishingDate,
                Publisher = itemEntity.Publisher,
                Author = userEntity.Author,
                MagazineName = userEntity.MagazineName,
                MagazinesIssueNumber = userEntity.MagazinesIssueNumber,
                CopyInfo = userEntity.CopyInfo
            };

            return userDto;
        }
    }
}
