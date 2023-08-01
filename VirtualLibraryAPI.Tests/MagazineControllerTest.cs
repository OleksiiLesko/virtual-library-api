using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Common;
using VirtualLibraryAPI.Library.Controllers;
using VirtualLibraryAPI.Models;
using VirtualLibraryAPI.Repository;

namespace VirtualLibraryAPI.Tests
{
    public class MagazineControllerTest
    {

        private readonly Mock<ILogger<MagazineController>> _logger;
        private readonly Mock<IMagazineModel> _magazineModel;
        private readonly Mock<IDepartmentModel> _departmentModel;
        private readonly MagazineController _controller;

        public MagazineControllerTest()
        {
            _logger = new Mock<ILogger<MagazineController>>();
            _magazineModel = new Mock<IMagazineModel>();
            _departmentModel = new Mock<IDepartmentModel>();
            _controller = new MagazineController(_logger.Object, _magazineModel.Object, _departmentModel.Object);
        }


        [Fact]
        public void GettAllMagazines_ReturnOK()
        {
            var magazines = new List<Domain.DTOs.Magazine>
        {
            new Domain.DTOs.Magazine { MagazineID = 1, IssueNumber = "20/20223" },
            new Domain.DTOs.Magazine { MagazineID = 2, IssueNumber = "20/20223" },
            new Domain.DTOs.Magazine { MagazineID = 3, IssueNumber = "20/20223" }
        };
            _magazineModel.Setup(model => model.GetAllMagazines()).Returns(magazines);


            var result = _controller.GetAllMagazines();

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void GettAllMagazines_ReturnNotFound()
        {
            List<Domain.DTOs.Magazine> magazines = null;
            _magazineModel.Setup(m => m.GetAllMagazines()).Returns(magazines);

            var result = _controller.GetAllMagazines();

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void GettAllMagazines_ReturnbBadRequest()
        {
            _magazineModel.Setup(model => model.GetAllMagazines()).Throws(new Exception("Failed to retrieve magazines"));

            var result = _controller.GetAllMagazines();

            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed", badRequestResult.Value);
        }
        [Fact]
        public void AddMagazine_ValidData_ReturnsOkResult()
        {
            var request = new Domain.DTOs.Magazine();
            var userType = UserType.Administrator;

            _departmentModel.Setup(m => m.GetDepartmentById(It.IsAny<int>())).Returns(new Domain.DTOs.Department { });

            _magazineModel.Setup(m => m.AddMagazine(It.IsAny<Domain.DTOs.Magazine>())).Returns(new Domain.DTOs.Magazine { });

            var result = _controller.AddMagazine( request);

            var okResult = Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void AddMagazine_ReturnNotFound()
        {
            var request = new Domain.DTOs.Magazine();
            var userType = UserType.Administrator;

            _departmentModel.Setup(m => m.GetDepartmentById(It.IsAny<int>())).Returns(new Domain.DTOs.Department { });

            _magazineModel.Setup(m => m.AddMagazine(It.IsAny<Domain.DTOs.Magazine>())).Returns((Domain.DTOs.Magazine)null);

            var result = _controller.AddMagazine(request);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void AddMagazine_ReturnBadRequest()
        {
            var request = new Domain.DTOs.Magazine
            {
                Name = "Magazine Name",
                PublishingDate = DateTime.Now,
                Publisher = "Publisher"
            };

            _magazineModel.Setup(model => model.AddMagazine(request)).Throws(new ArgumentException());

            var result = _controller.AddMagazine(request);

            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void AddCopyOfMagazineById_ReturnOK()
        {
            var magazineId = 1;
            var isAvailable = true;
            var addedMagazine = new Domain.DTOs.Copy
            {
                ItemID = 2,
                CopyID = magazineId
            };

            _magazineModel.Setup(model => model.AddCopyOfMagazineById(magazineId, isAvailable)).Returns(addedMagazine);

            var result = _controller.AddCopyOfMagazineById(magazineId);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void AddCopyOfMagazineById_ReturnNotFound()
        {
            var magazineId = 1;
            var isAvailable = true;
            _magazineModel.Setup(model => model.AddCopyOfMagazineById(magazineId, isAvailable)).Returns((Domain.DTOs.Copy)null);

            var result = _controller.AddCopyOfMagazineById(magazineId);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void AddCopyOfMagazineById_ReturnbBadRequest()
        {
            var magazineId = 1;
            var isAvailable = true;
            _magazineModel.Setup(model => model.AddCopyOfMagazineById(magazineId, isAvailable)).Throws(new Exception("Some error message"));


            var result = _controller.AddCopyOfMagazineById(magazineId);

            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed", badRequestResult.Value);
        }
        [Fact]
        public void GetMagazineById_ReturnOK()
        {

            var magazineId = 1;
            var expectedMagazine = new Domain.DTOs.Magazine
            {
                MagazineID = magazineId,
                IssueNumber = "20/2023"
            };

            _magazineModel.Setup(model => model.GetMagazineById(magazineId)).Returns(expectedMagazine);


            var result = _controller.GetMagazineById(magazineId);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void GetMagazineById_ReturnNotFound()
        {
            var magazineId = 1;
            _magazineModel.Setup(model => model.GetMagazineById(magazineId)).Returns(null as Domain.DTOs.Magazine);

            var result = _controller.GetMagazineById(magazineId);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void GetMagazineById_ReturnbBadRequest()
        {
            var magazineId = 1;
            _magazineModel.Setup(model => model.GetMagazineById(magazineId)).Throws(new Exception("Error retrieving book"));

            var result = _controller.GetMagazineById(magazineId);

            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void UpdateMagazine_ReturnOK()
        {
            int Id = 123;
            var request = new Domain.DTOs.Magazine();
            var userType = UserType.Administrator;

            _departmentModel.Setup(m => m.GetDepartmentById(It.IsAny<int>())).Returns(new Domain.DTOs.Department { });

            _magazineModel.Setup(m => m.UpdateMagazine(It.IsAny<int>(), It.IsAny<Domain.DTOs.Magazine>())).Returns(new Domain.DTOs.Magazine { });

            var result = _controller.UpdateMagazine(Id, request);

            var okResult = Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void UpdateMagazine_ReturnNotFound()
        {
            int userId = 123;
            var request = new Domain.DTOs.Magazine();
            var userType = UserType.Administrator;

            _departmentModel.Setup(m => m.GetDepartmentById(It.IsAny<int>())).Returns(new Domain.DTOs.Department { });

            _magazineModel.Setup(m => m.UpdateMagazine(It.IsAny<int>(), It.IsAny<Domain.DTOs.Magazine>())).Returns((Domain.DTOs.Magazine)null);

            var result = _controller.UpdateMagazine(userId, request);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void UpdateMagazine_ReturnbBadRequest()
        {
            var magazineId = 1;
            var request = new Domain.DTOs.Magazine
            {
                Name = "Updated Magazine Name",
                PublishingDate = DateTime.Now,
                Publisher = "Updated Publisher"
            };

            _magazineModel.Setup(model => model.UpdateMagazine(magazineId, request)).Throws(new Exception("Update failed"));

            var result = _controller.UpdateMagazine(magazineId, request);

            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void DeleteMagazine_ReturnNoContent()
        {
            var magazineId = 1;
            _magazineModel.Setup(model => model.GetMagazineById(magazineId)).Returns(new Domain.DTOs.Magazine());

            var result = _controller.DeleteMagazine(magazineId);

            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public void DeleteMagazine_ReturnNotFound()
        {
            var magazineId = 1;
            _magazineModel.Setup(model => model.GetMagazineById(magazineId)).Returns((Domain.DTOs.Magazine)null);

            var result = _controller.DeleteMagazine(magazineId);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void DeleteMagazine_ReturnsBadRequest_WhenExceptionThrown()
        {
            var magazineId = 1;
            _magazineModel.Setup(model => model.GetMagazineById(magazineId)).Throws<Exception>();

            var result = _controller.DeleteMagazine(magazineId);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
