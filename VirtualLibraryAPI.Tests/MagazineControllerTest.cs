using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Library.Controllers;
using VirtualLibraryAPI.Repository;

namespace VirtualLibraryAPI.Tests
{
    public class MagazineControllerTest
    {
        private readonly Mock<ILogger<MagazineController>> _loggerMock;
        private readonly Mock<ILogger<Models.Magazine>> _loggerMagazine;
        private readonly Mock<IMagazine> _magazineRepository;
        private readonly Models.Magazine _magazineModelMock;
        private readonly MagazineController _magazineController;

        public MagazineControllerTest()
        {
            _loggerMock = new Mock<ILogger<MagazineController>>();
            _loggerMagazine = new Mock<ILogger<Models.Magazine>>();
            _magazineRepository = new Mock<IMagazine>();
            _magazineModelMock = new Models.Magazine(_magazineRepository.Object, _loggerMagazine.Object);
            _magazineController = new MagazineController(_loggerMock.Object, _magazineModelMock);
        }


        [Fact]
        public void GettAllMagazines_ReturnOK()
        {
            var magazines = new List<Domain.Entities.Magazine>
        {
            new Domain.Entities.Magazine { ItemID = 1, IssueNumber = "20/20223" },
            new Domain.Entities.Magazine { ItemID = 2, IssueNumber = "20/20223" },
            new Domain.Entities.Magazine { ItemID = 3, IssueNumber = "20/20223" }
        };
            _magazineRepository.Setup(model => model.GetAllMagazines()).Returns(magazines);


            var result = _magazineController.GetAllMagazines();

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void GettAllMagazines_ReturnNotFound()
        {
            _magazineRepository.Setup(model => model.GetAllMagazines()).Returns(new List<Domain.Entities.Magazine>());

            var result = _magazineController.GetAllMagazines();

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void GettAllMagazines_ReturnbBadRequest()
        {
            _magazineRepository.Setup(model => model.GetAllMagazines()).Throws(new Exception("Failed to retrieve magazines"));

            var result = _magazineController.GetAllMagazines();

            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed", badRequestResult.Value);
        }
        [Fact]
        public void AddMagazine_ReturnOK()
        {
            var request = new Domain.DTOs.Magazine
            {
                Name = "Magazine Name",
                PublishingDate = DateTime.Now,
                Publisher = "Publisher"
            };

            var addedMagazine = new Domain.Entities.Magazine
            {
                ItemID = 1,
                IssueNumber = "20/20223"
            };

            _magazineRepository.Setup(model => model.AddMagazine(request)).Returns(addedMagazine);

            var result = _magazineController.AddMagazine(request);

            Assert.IsType<OkObjectResult>(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var magazineResponse = Assert.IsType<Domain.DTOs.Magazine>(okResult.Value);

            Assert.Equal(addedMagazine.ItemID, magazineResponse.MagazineID);
            Assert.Equal(request.Name, magazineResponse.Name);
            Assert.Equal(request.PublishingDate, magazineResponse.PublishingDate);
            Assert.Equal(request.Publisher, magazineResponse.Publisher);
            Assert.Equal(addedMagazine.IssueNumber, magazineResponse.IssueNumber);
        }
        [Fact]
        public void AddMagazine_ReturnNotFound()
        {
            var request = new Domain.DTOs.Magazine
            {
                Name = "Magazine Name",
                PublishingDate = DateTime.Now,
                Publisher = "Publisher"
            };

            _magazineRepository.Setup(model => model.AddMagazine(request)).Returns((Domain.Entities.Magazine)null);

            var result = _magazineController.AddMagazine(request);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void AddBook_ReturnBadRequest()
        {
            var request = new Domain.DTOs.Magazine
            {
                Name = "Magazine Name",
                PublishingDate = DateTime.Now,
                Publisher = "Publisher"
            };

            _magazineRepository.Setup(model => model.AddMagazine(request)).Throws(new ArgumentException());

            var result = _magazineController.AddMagazine(request);

            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void AddCopyOfMagazineById_ReturnOK()
        {
            var magazineId = 1;
            var addedMagazine = new Domain.Entities.Copy
            {
                ItemID = 2,
                CopyID = magazineId
            };

            _magazineRepository.Setup(model => model.AddCopyOfMagazineById(magazineId)).Returns(addedMagazine);

            var result = _magazineController.AddCopyOfMagazineById(magazineId);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void AddCopyOfMagazineById_ReturnNotFound()
        {
            var magazineId = 1;
            _magazineRepository.Setup(model => model.AddCopyOfMagazineById(magazineId)).Returns((Domain.Entities.Copy)null);

            var result = _magazineController.AddCopyOfMagazineById(magazineId);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void AddCopyOfMagazineById_ReturnbBadRequest()
        {
            var magazineId = 1;
            _magazineRepository.Setup(model => model.AddCopyOfMagazineById(magazineId)).Throws(new Exception("Some error message"));


            var result = _magazineController.AddCopyOfMagazineById(magazineId);

            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed", badRequestResult.Value);
        }
        [Fact]
        public void GetMagazineById_ReturnOK()
        {

            var magazineId = 1;
            var expectedMagazine = new Domain.Entities.Magazine
            {
                ItemID = magazineId,
                IssueNumber = "20/2023"
            };

            _magazineRepository.Setup(model => model.GetMagazineById(magazineId)).Returns(expectedMagazine);


            var result = _magazineController.GetMagazineById(magazineId);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void GetMagazineById_ReturnNotFound()
        {
            var magazineId = 1;
            _magazineRepository.Setup(model => model.GetMagazineById(magazineId)).Returns(null as Domain.Entities.Magazine);

            var result = _magazineController.GetMagazineById(magazineId);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void GetMagazineById_ReturnbBadRequest()
        {
            var magazineId = 1;
            _magazineRepository.Setup(model => model.GetMagazineById(magazineId)).Throws(new Exception("Error retrieving book"));

            var result = _magazineController.GetMagazineById(magazineId);

            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void UpdateMagazine_ReturnOK()
        {
            var magazineId = 1;
            var request = new Domain.DTOs.Magazine
            {
                Name = "Updated Magazine Name",
                PublishingDate = DateTime.Now,
                Publisher = "Updated Publisher"
            };
            var updatedMagazine = new Domain.Entities.Magazine
            {
                ItemID = magazineId,
                IssueNumber = "20/2023"
            };

            _magazineRepository.Setup(model => model.UpdateMagazine(magazineId, request)).Returns(updatedMagazine);

            var result = _magazineController.UpdateMagazine(magazineId, request);

            Assert.IsType<OkObjectResult>(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var magazineResponse = Assert.IsType<Domain.DTOs.Magazine>(okResult.Value);

            Assert.Equal(updatedMagazine.ItemID, magazineResponse.MagazineID);
            Assert.Equal(request.Name, magazineResponse.Name);
            Assert.Equal(updatedMagazine.IssueNumber, magazineResponse.IssueNumber);
            Assert.Equal(request.Publisher, magazineResponse.Publisher);
            Assert.Equal(request.PublishingDate, magazineResponse.PublishingDate);
        }
        [Fact]
        public void UpdateBook_ReturnNotFound()
        {
            var magazineId = 1;
            var request = new Domain.DTOs.Magazine
            {
                Name = "Updated Magazine Name",
                PublishingDate = DateTime.Now,
                Publisher = "Updated Publisher"
            };

            _magazineRepository.Setup(model => model.UpdateMagazine(magazineId, request)).Returns((Domain.Entities.Magazine)null);

            var result = _magazineController.UpdateMagazine(magazineId, request);

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

            _magazineRepository.Setup(model => model.UpdateMagazine(magazineId, request)).Throws(new Exception("Update failed"));

            var result = _magazineController.UpdateMagazine(magazineId, request);

            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void DeleteMagazine_ReturnNoContent()
        {
            var magazineId = 1;
            _magazineRepository.Setup(model => model.GetMagazineById(magazineId)).Returns(new Domain.Entities.Magazine());

            var result = _magazineController.DeleteMagazine(magazineId);

            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public void DeleteMagazine_ReturnNotFound()
        {
            var magazineId = 1;
            _magazineRepository.Setup(model => model.GetMagazineById(magazineId)).Returns((Domain.Entities.Magazine)null);

            var result = _magazineController.DeleteMagazine(magazineId);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void DeleteMagazine_ReturnsBadRequest_WhenExceptionThrown()
        {
            var magazineId = 1;
            _magazineRepository.Setup(model => model.GetMagazineById(magazineId)).Throws<Exception>();

            var result = _magazineController.DeleteMagazine(magazineId);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
