using Microsoft.AspNetCore.Mvc;
using VirtualLibraryAPI.Common;
using VirtualLibraryAPI.Library.Middleware;
using VirtualLibraryAPI.Models;

namespace VirtualLibraryAPI.Library.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<UserController> _logger;
        /// <summary>
        /// User model
        /// </summary>
        private readonly IUserModel _userModel;
        /// <summary>
        /// User model
        /// </summary>
        private readonly IDepartmentModel _departmentModel;
        /// <summary>
        /// Constructor with logger,context and model
        /// </summary>
        /// <param name="logger"></param>
        public UserController(ILogger<UserController> logger, IUserModel userModel,IDepartmentModel departmentModel)
        {
            _logger = logger;
            _userModel = userModel;
            _departmentModel = departmentModel;
        }
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                _logger.LogInformation("Get all users ");
                var users = _userModel.GetAllUsers();
                if (users == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Users received ");
                return Ok(_userModel.GetAllUsersResponse());
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }
        /// <summary>
        /// Add user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [MiddlewareFilter(typeof(UserAuthorizationMiddleware))]
        public IActionResult AddUser([FromHeader] int userid, [FromBody] Domain.DTOs.User request, UserType userType)
        {
            try
            {
                var department = _departmentModel.GetDepartmentById(request.DepartmentID);
                if (department == null)
                {
                    return BadRequest("Invalid DepartmentID. Department with the specified ID does not exist.");
                }
                var addedArticle = _userModel.AddUser(request, userType);
                if (addedArticle == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Adding user:{UserID}", addedArticle.UserID);

                _logger.LogInformation("Article added");
                return Ok(new Domain.DTOs.User
                {
                   UserID = addedArticle.UserID,
                   DepartmentID = addedArticle.DepartmentID,
                   FirstName = addedArticle.FirstName,
                   LastName = addedArticle.LastName,
                   UserType = userType
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }
        /// <summary>
        ///  Get user by Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var article = _userModel.GetUserById(id);

                if (article == null)
                {
                    return NotFound();
                }

                _logger.LogInformation("Getting user by ID:{UserID}", article.UserID);
                _logger.LogInformation("User received ");
                return Ok(_userModel.GetUserByIdResponse(id));
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }
        /// <summary>
        /// Update user by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, [FromBody] Domain.DTOs.User request,UserType userType)
        {
            try
            {
                var department = _departmentModel.GetDepartmentById(request.DepartmentID);
                if (department == null)
                {
                    return BadRequest("Invalid DepartmentID. Department with the specified ID does not exist.");
                }
                var updatedUser = _userModel.UpdateUser(id, request, userType);
                if (updatedUser == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Updating user by ID:{UserID}", updatedUser.UserID);
                _logger.LogInformation("User data updated ");

                return Ok(new Domain.DTOs.User
                {
                    UserID = updatedUser.UserID,
                    DepartmentID = updatedUser.DepartmentID,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    UserType = userType
                });
            }

            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }
        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            try
            {
                var article = _userModel.GetUserById(id);
                if (article == null)
                {
                    return NotFound();
                }

                _userModel.DeleteUser(id);
                _logger.LogInformation("Deleting user by ID:{UserID}", article.UserID);
                _logger.LogInformation("User deleted ");

                return NoContent();
            }

            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }
    }
}
