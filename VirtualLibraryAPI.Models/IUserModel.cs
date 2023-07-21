using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Common;

namespace VirtualLibraryAPI.Models
{
    /// <summary>
    /// Interface for user
    /// </summary>
    public interface IUserModel
    {
        /// <summary>
        /// Method for get all users
        /// </summary>
        /// <returns></returns>
        IEnumerable<Domain.DTOs.User> GetAllUsers();
        /// <summary>
        /// Method for get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.DTOs.User GetUserById(int id);
        /// <summary>
        /// Method for add user
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        Domain.DTOs.User AddUser(Domain.DTOs.User user, UserType userType,DepartmentType departmentType);
        /// <summary>
        /// Method for delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.DTOs.User DeleteUser(int id);
        /// <summary>
        /// Get all users for response DTO
        /// </summary>
        /// <returns></returns>
        IEnumerable<Domain.DTOs.User> GetAllUsersResponse();
        /// <summary>
        /// Get  user by id in model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.DTOs.User GetUserByIdResponse(int id);
        /// <summary>
        /// Method for update user data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="article"></param>
        /// <returns></returns>
        Domain.DTOs.User UpdateUser(int id, Domain.DTOs.User user, UserType userType, DepartmentType departmentType);
    }
}
