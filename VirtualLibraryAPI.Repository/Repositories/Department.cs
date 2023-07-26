using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Domain.DTOs;
using VirtualLibraryAPI.Domain;
using VirtualLibraryAPI.Domain.Entities;

namespace VirtualLibraryAPI.Repository.Repositories
{
    /// <summary>
    /// Department repository 
    /// </summary>
    public class Department : IDepartmentRepository
    {
        /// <summary>
        /// Application context
        /// </summary>
        private readonly ApplicationContext _context;
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<Department> _logger;
        /// <summary>
        /// Constructor with context and logger
        /// </summary>
        /// <param name="context"></param>
        public Department(ApplicationContext context, ILogger<Department> logger)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// Add department to the database
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.DTOs.Department AddDepartment(Domain.DTOs.Department department)
        {
            var newDepartment = new Domain.Entities.Department()
            {
                DepartmentID = department.DepartmentID,
                DepartmentName = department.DepartmentName,
            };
            _context.Departments.Add(newDepartment);
            _context.SaveChanges();
            _logger.LogInformation("Adding department to the database: {DepartmentID}", newDepartment.DepartmentID);

            var addedDepartment = new Domain.DTOs.Department
            {
                DepartmentID = newDepartment.DepartmentID,
               DepartmentName = newDepartment.DepartmentName,
            };

            return addedDepartment;
        }


        /// <summary>
        /// Deleting department from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.DTOs.Department DeleteDepartment(int id)
        {
            var department = _context.Departments.Find(id);
            _context.Departments.Remove(department);

            var deletedDepartmentDto = new Domain.DTOs.Department
            {
                DepartmentID = department.DepartmentID,
               DepartmentName = department.DepartmentName
            };

            _context.Departments.Remove(department);
            _context.SaveChanges();
            _logger.LogInformation("Deleting department from database: {DepartmentID}", department.DepartmentID);

            return deletedDepartmentDto;
        }
        /// <summary>
        /// Returning all departments from the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<Domain.DTOs.Department> GetAllDepartments()
        {
            var departmentEntities = _context.Departments.ToList();
            var departmentDtos = new List<Domain.DTOs.Department>();

            foreach (var departmentEntity in departmentEntities)
            {
                var departmentDto = new Domain.DTOs.Department();

                departmentDtos.Add(departmentDto);
            }
            _logger.LogInformation("Returning all departmens from the database");


            return departmentDtos;
        }
        /// <summary>
        /// Get all departments for response DTO
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<Domain.DTOs.Department> GetAllDepartmentsResponse()
        {
            _logger.LogInformation("Get all departmens for response DTO:");

            var departmens = _context.Departments
                .Select(x => new Domain.DTOs.Department
                {
                    DepartmentID = x.DepartmentID,
                    DepartmentName = x.DepartmentName
                })
                .ToList();
            return departmens;
        }
        /// <summary>
        /// Get department by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.DTOs.Department GetDepartmentById(int id)
        {
            _logger.LogInformation($"Getting department id: DepartmentID {id}");

            var departmentEntity = _context.Departments.FirstOrDefault(b => b.DepartmentID == id);
            if (departmentEntity == null)
            {
                return null;
            }

           
            var departmentDto = new Domain.DTOs.Department
            {
                DepartmentID = departmentEntity.DepartmentID,
                DepartmentName = departmentEntity.DepartmentName,
               
            };

            return departmentDto;
        }
        /// <summary>
        /// Update department
        /// </summary>
        /// <param name="id"></param>
        /// <param name="department"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.DTOs.Department UpdateDepartment(int id, Domain.DTOs.Department department)
        {
            var existingDepartment = _context.Departments.Find(id);

            existingDepartment.DepartmentName = department.DepartmentName;

            _context.SaveChanges();
            _logger.LogInformation("Update department by id in the database: {DepartmentID}", existingDepartment.DepartmentID);

            var departmentDto = new Domain.DTOs.Department
            {
                DepartmentID = existingDepartment.DepartmentID,
                DepartmentName = existingDepartment.DepartmentName,

            };

            return departmentDto;
        }
      
    }
}
