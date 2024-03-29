﻿using Microsoft.AspNetCore.Mvc;
using VirtualLibraryAPI.Common;
using VirtualLibraryAPI.Domain.DTOs;
using VirtualLibraryAPI.Models;

namespace VirtualLibraryAPI.Library.Controllers
{
    /// <summary>
    /// Magazine controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MagazineController : ControllerBase
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<MagazineController> _logger;
        /// <summary>
        /// Magazine model
        /// </summary>
        private readonly IMagazineModel _magazineModel;
        /// <summary>
        /// Department model
        /// </summary>
        private readonly IDepartmentModel _departmentModel;

        /// <summary>
        /// Constructor with logger,context and model
        /// </summary>
        /// <param name="logger"></param>
        public MagazineController(ILogger<MagazineController> logger, IMagazineModel magazineModel,IDepartmentModel departmentModel)
        {
            _logger = logger;
            _magazineModel = magazineModel;
            _departmentModel = departmentModel;

        }
        /// <summary>
        /// Get all magazines
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllMagazines()
        {
            try
            {
                _logger.LogInformation("Get all magazines ");
                var magazines = _magazineModel.GetAllMagazines();
                if (magazines == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Magazines received ");
                return Ok(_magazineModel.GetAllMagazinesResponse());
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }
        /// <summary>
        /// Add magazine
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddMagazine([FromBody] Domain.DTOs.Magazine request)
        {
            try
            {
                var department = _departmentModel.GetDepartmentById(request.DepartmentID);
                if (department == null)
                {
                    return BadRequest("Invalid DepartmentID. Department with the specified ID does not exist.");
                }
                var addedMagazine = _magazineModel.AddMagazine(request);
                if (addedMagazine == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Adding magazine:{MagazineID}", addedMagazine.MagazineID);

                _logger.LogInformation("Magazine added");
                return Ok(new Domain.DTOs.Magazine
                {
                    MagazineID = addedMagazine.MagazineID,
                    DepartmentID = addedMagazine.DepartmentID,
                    Name = request.Name,
                    PublishingDate = request.PublishingDate,
                    Publisher = request.Publisher,
                    IssueNumber = addedMagazine.IssueNumber
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }
        /// <summary>
        /// Add copies of a magazine by id
        /// </summary>
        /// <returns></returns>
        [HttpPost("{id}/Copy")]
        public IActionResult AddCopyOfMagazineById(int id)
        {
            try
            {
                var addedMagazine = _magazineModel.AddCopyOfMagazineById(id, isAvailable: true);
                if (addedMagazine == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Adding copy:{CopyID}", id);

                _logger.LogInformation("Copy added");
                return Ok(_magazineModel.AddCopyOfMagazineByIdResponse(id));
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }
        /// <summary>
        ///  Get magazine by Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetMagazineById(int id)
        {
            try
            {
                var magazine = _magazineModel.GetMagazineById(id);

                if (magazine == null)
                {
                    return NotFound();
                }

                _logger.LogInformation("Getting magazine by ID:{MagazineID}", magazine.MagazineID);
                _logger.LogInformation("Magazine received ");
                return Ok(_magazineModel.GetMagazineByIdResponse(id));
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }
        /// <summary>
        /// Update magazine by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult UpdateMagazine(int id, [FromBody] Domain.DTOs.Magazine request)
        {
            try
            {
                var department = _departmentModel.GetDepartmentById(request.DepartmentID);
                if (department == null)
                {
                    return BadRequest("Invalid DepartmentID. Department with the specified ID does not exist.");
                }
                var updatedMagazine = _magazineModel.UpdateMagazine(id, request);
                if (updatedMagazine == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Updating magazine by ID:{MagazineID}", updatedMagazine.MagazineID);
                _logger.LogInformation("Magazine updated ");

                return Ok(new Domain.DTOs.Magazine
                {
                    MagazineID = updatedMagazine.MagazineID,
                    DepartmentID = updatedMagazine.DepartmentID,
                    Name = request.Name,
                    Publisher = request.Publisher,
                    PublishingDate = request.PublishingDate,
                    IssueNumber = updatedMagazine.IssueNumber
                });
            }

            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }
        /// <summary>
        /// Delete magazine
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteMagazine(int id)
        {
            try
            {
                var magazine = _magazineModel.GetMagazineById(id);
                if (magazine == null)
                {
                    return NotFound();
                }

                _magazineModel.DeleteMagazine(id);
                _logger.LogInformation("Deleting magazine by ID:{MagazineID}", magazine.MagazineID);
                _logger.LogInformation("Magazine deleted ");

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
