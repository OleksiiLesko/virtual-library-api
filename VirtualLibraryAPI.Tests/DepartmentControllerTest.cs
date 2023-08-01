using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Library.Controllers;
using VirtualLibraryAPI.Models;

namespace VirtualLibraryAPI.Tests
{
    public class DepartmentControllerTest
    {
        private readonly Mock<ILogger<DepartmentController>> _logger;
        private readonly Mock<IDepartmentModel> _departmentModel;
        private readonly DepartmentController _controller;

        public DepartmentControllerTest()
        {
            _logger = new Mock<ILogger<DepartmentController>>();
            _departmentModel = new Mock<IDepartmentModel>();
            _controller = new DepartmentController(_logger.Object, _departmentModel.Object);
        }


        [Fact]
        public void GettAllDepartments_ReturnOK()
        {
            var departments = new List<Domain.DTOs.Department>
        {
            new Domain.DTOs.Department { DepartmentID = 1, DepartmentName = "20/20223" },
            new Domain.DTOs.Department { DepartmentID = 2, DepartmentName = "20/20223" },
            new Domain.DTOs.Department { DepartmentID = 3, DepartmentName = "20/20223" }
        };
            _departmentModel.Setup(model => model.GetAllDepartments()).Returns(departments);


            var result = _controller.GetAllDepartments();

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void GettAllDepartments_ReturnNotFound()
        {
            List<Domain.DTOs.Department> departments = null;
            _departmentModel.Setup(m => m.GetAllDepartments()).Returns(departments);

            var result = _controller.GetAllDepartments();

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void GettAllDepartments_ReturnbBadRequest()
        {
            _departmentModel.Setup(model => model.GetAllDepartments()).Throws(new Exception("Failed to retrieve magazines"));

            var result = _controller.GetAllDepartments();

            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed", badRequestResult.Value);
        }
        [Fact]
        public void AddDepartment_ValidData_ReturnsOkResult()
        {
            var request = new Domain.DTOs.Department();

            _departmentModel.Setup(m => m.AddDepartment(It.IsAny<Domain.DTOs.Department>())).Returns(new Domain.DTOs.Department { });

            var result = _controller.AddDepartment(request);

            var okResult = Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void AddDepartment_ReturnNotFound()
        {
            var request = new Domain.DTOs.Department();

            _departmentModel.Setup(m => m.AddDepartment(It.IsAny<Domain.DTOs.Department>())).Returns((Domain.DTOs.Department)null);

            var result = _controller.AddDepartment(request);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void AddDepartment_ReturnBadRequest()
        {
            var request = new Domain.DTOs.Department
            {
                DepartmentName = "Magazine Name"
            };

            _departmentModel.Setup(model => model.AddDepartment(request)).Throws(new ArgumentException());

            var result = _controller.AddDepartment(request);

            Assert.IsType<BadRequestObjectResult>(result);
        }
       
        [Fact]
        public void GetDepartmentById_ReturnOK()
        {

            var departmentId = 1;
            var expectedDepartment = new Domain.DTOs.Department
            {
                DepartmentID = departmentId,
                DepartmentName = "20/2023"
            };

            _departmentModel.Setup(model => model.GetDepartmentById(departmentId)).Returns(expectedDepartment);


            var result = _controller.GetDepartmentById(departmentId);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void GetDepartmentById_ReturnNotFound()
        {
            var departmentId = 1;
            _departmentModel.Setup(model => model.GetDepartmentById(departmentId)).Returns(null as Domain.DTOs.Department);

            var result = _controller.GetDepartmentById(departmentId);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void GetDepartmentById_ReturnbBadRequest()
        {
            var departmentId = 1;
            _departmentModel.Setup(model => model.GetDepartmentById(departmentId)).Throws(new Exception("Error retrieving book"));

            var result = _controller.GetDepartmentById(departmentId);

            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void UpdateDepartment_ReturnOK()
        {
            int Id = 123;
            var request = new Domain.DTOs.Department();

            _departmentModel.Setup(m => m.UpdateDepartment(It.IsAny<int>(), It.IsAny<Domain.DTOs.Department>())).Returns(new Domain.DTOs.Department { });

            var result = _controller.UpdateDepartment(Id, request);

            var okResult = Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void UpdateDepartment_ReturnNotFound()
        {
            int userId = 123;
            var request = new Domain.DTOs.Department();

            _departmentModel.Setup(m => m.UpdateDepartment(It.IsAny<int>(), It.IsAny<Domain.DTOs.Department>())).Returns((Domain.DTOs.Department)null);

            var result = _controller.UpdateDepartment(userId, request);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void UpdateDepartment_ReturnbBadRequest()
        {
            var Id = 1;
            var request = new Domain.DTOs.Department
            {
                DepartmentName = "Updated Magazine Name",
            };

            _departmentModel.Setup(model => model.UpdateDepartment(Id, request)).Throws(new Exception("Update failed"));

            var result = _controller.UpdateDepartment(Id, request);

            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void DeleteDepartment_ReturnNoContent()
        {
            var departmentId = 1;
            _departmentModel.Setup(model => model.GetDepartmentById(departmentId)).Returns(new Domain.DTOs.Department());

            var result = _controller.DeleteDepartment(departmentId);

            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public void DeleteDepartment_ReturnNotFound()
        {
            var departmentId = 1;
            _departmentModel.Setup(model => model.GetDepartmentById(departmentId)).Returns((Domain.DTOs.Department)null);

            var result = _controller.DeleteDepartment(departmentId);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void DeleteDepartment_ReturnsBadRequest_WhenExceptionThrown()
        {
            var departmentId = 1;
            _departmentModel.Setup(model => model.GetDepartmentById(departmentId)).Throws<Exception>();

            var result = _controller.DeleteDepartment(departmentId);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
