using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Repository;

namespace VirtualLibraryAPI.Models
{
    /// <summary>
    /// Model for department
    /// </summary>
    public class DepartmentModel : IDepartmentModel
    {
        /// <summary>
        /// Using department repository
        /// </summary>
        private readonly IDepartmentRepository _repository;
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<DepartmentModel> _logger;

        /// <summary>
        /// Constructor with book Repository and logger
        /// </summary>
        /// <param name="book"></param>
        /// <param name="logger"></param>
        public DepartmentModel(ILogger<DepartmentModel> logger, IDepartmentRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        /// <summary>
        /// Adding department from Department model
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public Domain.DTOs.Department AddDepartment(Domain.DTOs.Department department)
        {
            _logger.LogInformation($"Adding department from Department model {department}");
            var result = _repository.AddDepartment(department);
            if (result == null)
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// Updating department from department model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="department"></param>
        /// <returns></returns>

        public Domain.DTOs.Department UpdateDepartment(int id, Domain.DTOs.Department department)
        {
            _logger.LogInformation($"Updating department from Department model: DepartmentID {id}");
            var result = _repository.UpdateDepartment(id, department);
            if (result == null)
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// Deleting department from Department model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public Domain.DTOs.Department DeleteDepartment(int id)
        {
            _logger.LogInformation($"Deleting department from Department model: DepartmentID {id}");
            var result = _repository.DeleteDepartment(id);
            if (result == null)
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// Getting all departments from Department model
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Domain.DTOs.Department> GetAllDepartments()
        {
            _logger.LogInformation($"Getting all departments from Department model ");
            var result = _repository.GetAllDepartments();
            if (result.Any())
            {
                return result;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Getting department by id from Department model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public Domain.DTOs.Department GetDepartmentById(int id)
        {
            _logger.LogInformation($"Getting department by id from department model: DepartmentID {id} ");
            var result = _repository.GetDepartmentById(id);
            if (result == null)
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// Get all departmens for response DTO
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Domain.DTOs.Department> GetAllDepartmentsResponse()
        {
            _logger.LogInformation("Get all departmens for response DTO from Departmen model  ");
            var result = _repository.GetAllDepartmentsResponse();
            if (result == null)
            {
                return result;
            }
            return result;
        }
    }
}
