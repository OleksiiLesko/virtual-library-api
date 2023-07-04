using Microsoft.AspNetCore.Mvc;
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
        /// Article model
        /// </summary>
        private readonly IUserModel _model;
        /// <summary>
        /// Constructor with logger,context and model
        /// </summary>
        /// <param name="logger"></param>
        public UserController(ILogger<UserController> logger, IUserModel model)
        {
            _logger = logger;
            _model = model;
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
                var users = _model.GetAllUsers();
                if (users == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Users received ");
                return Ok(_model.GetAllUsersResponse());
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
        public IActionResult AddUser([FromBody] Domain.DTOs.User request)
        {
            try
            {
                var addedArticle = _model.AddUser(request);
                if (addedArticle == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Adding user:{UserID}", addedArticle.UserID);

                _logger.LogInformation("Article added");
                return Ok(new Domain.DTOs.User
                {
                   UserID = addedArticle.UserID,
                   FirstName = addedArticle.FirstName,
                   LastName = addedArticle.LastName,
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
                var article = _model.GetUserById(id);

                if (article == null)
                {
                    return NotFound();
                }

                _logger.LogInformation("Getting user by ID:{UserID}", article.UserID);
                _logger.LogInformation("User received ");
                return Ok(_model.GetUserByIdResponse(id));
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
        public ActionResult UpdateUser(int id, [FromBody] Domain.DTOs.User request)
        {
            try
            {
                var updatedUser = _model.UpdateUser(id, request);
                if (updatedUser == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Updating user by ID:{UserID}", updatedUser.UserID);
                _logger.LogInformation("User data updated ");

                return Ok(new Domain.DTOs.User
                {
                    UserID = updatedUser.UserID,
                    FirstName = request.FirstName,
                    LastName = request.LastName
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
                var article = _model.GetUserById(id);
                if (article == null)
                {
                    return NotFound();
                }

                _model.DeleteUser(id);
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
