using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLibraryAPI.Models
{
    /// <summary>
    /// Department model interface 
    /// </summary>
    public interface IDepartmentModel
    {
        /// <summary>
        /// Method for get all departmens
        /// </summary>
        /// <returns></returns>
        IEnumerable<Domain.DTOs.Department> GetAllDepartments();
        /// <summary>
        /// Method for get department by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.DTOs.Department GetDepartmentById(int id);
        /// <summary>
        /// Method for add department
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        Domain.DTOs.Department AddDepartment(Domain.DTOs.Department department);
        /// <summary>
        /// Method for update department
        /// </summary>
        /// <param name="id"></param>
        /// <param name="department"></param>
        /// <returns></returns>
        Domain.DTOs.Department UpdateDepartment(int id, Domain.DTOs.Department department);
        /// <summary>
        /// Method for delete department
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.DTOs.Department DeleteDepartment(int id);
        /// <summary>
        /// Method for response get all departmens
        /// </summary>
        /// <returns></returns>
        IEnumerable<Domain.DTOs.Department> GetAllDepartmentsResponse();
    }
}
