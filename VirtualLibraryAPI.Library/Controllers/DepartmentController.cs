using Microsoft.AspNetCore.Mvc;
using VirtualLibraryAPI.Models;

namespace VirtualLibraryAPI.Library.Controllers
{
    /// <summary>
    ///  Department controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<DepartmentController> _logger;
        /// <summary>
        /// Book model
        /// </summary>
        private readonly IDepartmentModel _model;

        /// <summary>
        /// Constructor with logger,context and model
        /// </summary>
        /// <param name="logger"></param>
        public DepartmentController(ILogger<DepartmentController> logger, IDepartmentModel model)
        {
            _logger = logger;
            _model = model;
        }
        /// <summary>
        /// Get all departmens
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllDepartments()
        {
            try
            {
                _logger.LogInformation("Get all departments ");
                var departments = _model.GetAllDepartments();
                if (departments == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Departments received ");
                return Ok(_model.GetAllDepartmensResponse());
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }
        /// <summary>
        /// Add Department
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddDepartment([FromBody] Domain.DTOs.Department request)
        {
            try
            {
                var addedDepartment = _model.AddDepartment(request);
                if (addedDepartment == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Adding department:{DepartmentID}", addedDepartment.DepartmentID);

                _logger.LogInformation("Department added");
                return Ok(new Domain.DTOs.Department
                {
                   DepartmentID = addedDepartment.DepartmentID,
                   DepartmentName = addedDepartment.DepartmentName,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }

        /// <summary>
        ///  Get department by Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetDepartmentById(int id)
        {
            try
            {
                var department = _model.GetDepartmentById(id);

                if (department == null)
                {
                    return NotFound();
                }

                _logger.LogInformation("Getting department by ID:{DepartmentID}", department.DepartmentID);
                _logger.LogInformation("Department received ");
                return Ok(_model.GetDepartmentById(id));
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }
        /// <summary>
        /// Update department by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult UpdateDepartment(int id, [FromBody] Domain.DTOs.Department request)
        {
            try
            {
                var updatedDepartment = _model.UpdateDepartment(id, request);
                if (updatedDepartment == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Updating department by ID:{DepartmentID}", updatedDepartment.DepartmentID);
                _logger.LogInformation("Department updated ");

                return Ok(updatedDepartment);
            }

            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }
        /// <summary>
        /// Delete department
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteDepartment(int id)
        {
            try
            {
                var department = _model.GetDepartmentById(id);
                if (department == null)
                {
                    return NotFound();
                }

                _model.DeleteDepartment(id);
                _logger.LogInformation("Deleting department by ID:{DepartmentID}", department.DepartmentID);
                _logger.LogInformation("Department deleted ");

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
