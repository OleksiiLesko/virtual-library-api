using Microsoft.AspNetCore.Mvc;
using VirtualLibraryAPI.Library.Middleware;
using VirtualLibraryAPI.Models;

namespace VirtualLibraryAPI.Library.Controllers
{
    /// <summary>
    /// Client controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<ClientController> _logger;
        /// <summary>
        /// Article model
        /// </summary>
        private readonly IClientModel _model;
        /// <summary>
        /// Constructor with logger,context and model
        /// </summary>
        /// <param name="logger"></param>
        public ClientController(ILogger<ClientController> logger, IClientModel model)
        {
            _logger = logger;
            _model = model;
        }
        /// <summary>
        /// Get all clients
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllClients()
        {
            try
            {
                _logger.LogInformation("Get all clients ");
                var users = _model.GetAllClients();
                if (users == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Clients received ");
                return Ok(_model.GetAllClientsResponse());
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }
        /// <summary>
        /// Add client
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddClient([FromBody] Domain.DTOs.Client request)
        {
            try
            {
                var addedClient = _model.AddClient(request);
                if (addedClient == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Adding client:{ClientID}", addedClient.ClientID);

                _logger.LogInformation("Client added");
                return Ok(new Domain.DTOs.Client
                {
                    ClientID = addedClient.ClientID,
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
        ///  Get client by Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetClientById(int id)
        {
            try
            {
                var client = _model.GetClientById(id);

                if (client == null)
                {
                    return NotFound();
                }

                _logger.LogInformation("Getting client by ID:{ClientID}", client.ClientID);
                _logger.LogInformation("Client received ");
                return Ok(_model.GetClientByIdResponse(id));
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }
        /// <summary>
        /// Update client by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult UpdateClient(int id, [FromBody] Domain.DTOs.Client request)
        {
            try
            {
                var updatedClient = _model.UpdateClient(id, request);
                if (updatedClient == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Updating client by ID:{ClientID}", updatedClient.ClientID);
                _logger.LogInformation("User data updated ");

                return Ok(new Domain.DTOs.Client
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                });
            }

            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }
        /// <summary>
        /// Delete client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteClient(int id)
        {
            try
            {
                var article = _model.GetClientById(id);
                if (article == null)
                {
                    return NotFound();
                }

                _model.DeleteClient(id);
                _logger.LogInformation("Deleting client by ID:{ClientID}", article.ClientID);
                _logger.LogInformation("Client deleted ");

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
