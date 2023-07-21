using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Repository;

namespace VirtualLibraryAPI.Tests
{
    public class MagazineModelTest
    {
        private readonly ILogger<Models.MagazineModel> _logger;
        private readonly Mock<IMagazineRepository> _magazineRepository;

        public MagazineModelTest()
        {
            _magazineRepository = new Mock<IMagazineRepository>();
            _logger = new Mock<ILogger<Models.MagazineModel>>().Object;
        }

        //[Fact]
        //public void AddMagazine_ReturnsAddedMagazine()
        //{
        //    var magazineDto = new Domain.DTOs.Magazine { MagazineID = 2, IssueNumber = "40/2023" };
        //    var addedMagazine = new Domain.DTOs.Magazine { MagazineID = 1, IssueNumber = "40/2023" };
        //    _magazineRepository.Setup(x => x.AddMagazine(magazineDto)).Returns(addedMagazine);
        //    var magazineModel = new Models.MagazineModel(_logger, _magazineRepository.Object);

        //    var result = magazineModel.AddMagazine(magazineDto);

        //    Assert.NotNull(result);
        //    Assert.Equal(magazineDto.IssueNumber, result.IssueNumber);
        //    Assert.NotEqual(magazineDto.MagazineID, result.MagazineID);
        //    _magazineRepository.Verify(x => x.AddMagazine(magazineDto), Times.Once());
        //}
        //[Fact]
        //public void UpdateMagazine_Should_Return_Updated_Magazine()
        //{
        //    var magazineID = 1;
        //    var magazineDto = new Domain.DTOs.Magazine { MagazineID = 2, IssueNumber = "40/2023" };
        //    var updatedMagazine = new Domain.DTOs.Magazine { MagazineID = 2, IssueNumber = "40/2023" };
        //    _magazineRepository.Setup(x => x.UpdateMagazine(magazineID, magazineDto)).Returns(updatedMagazine);
        //    var bookModel = new Models.MagazineModel(_logger, _magazineRepository.Object);

        //    var result = bookModel.UpdateMagazine(magazineID, magazineDto);

        //    Assert.Equal(updatedMagazine.MagazineID, result.MagazineID);
        //    Assert.Equal(updatedMagazine.IssueNumber, result.IssueNumber);
        //    _magazineRepository.Verify(x => x.UpdateMagazine(magazineID, magazineDto), Times.Once());
        //}
        [Fact]
        public void DeleteMagazine_ReturnsDeletedMagazine()
        {
            int magazineIdToDelete = 1;
            var expectedDeletedMagazine = new Domain.DTOs.Magazine { MagazineID = magazineIdToDelete,IssueNumber = "40/2023" };
            _magazineRepository.Setup(x => x.DeleteMagazine(magazineIdToDelete)).Returns(expectedDeletedMagazine);
            var magazineModel = new Models.MagazineModel(_logger, _magazineRepository.Object);

            var deletedMagazine = magazineModel.DeleteMagazine(magazineIdToDelete);

            _magazineRepository.Verify(x => x.DeleteMagazine(magazineIdToDelete), Times.Once());
            Assert.Equal(expectedDeletedMagazine, deletedMagazine);
        }
        [Fact]
        public void GetAllMagazines_ReturnsAllMagazines()
        {
            var expectedMagazines = new List<Domain.DTOs.Magazine>
        {
            new Domain.DTOs.Magazine { MagazineID = 1,  IssueNumber = "40/2023" },
            new Domain.DTOs.Magazine { MagazineID = 2,  IssueNumber = "40/2023" },
            new Domain.DTOs.Magazine { MagazineID = 3, IssueNumber = "40/2023" }
        };
            _magazineRepository.Setup(x => x.GetAllMagazines()).Returns(expectedMagazines);
            var magazineModel = new Models.MagazineModel(_logger, _magazineRepository.Object);

            var allMagazines = magazineModel.GetAllMagazines();

            _magazineRepository.Verify(x => x.GetAllMagazines(), Times.Once());
            Assert.Equal(expectedMagazines, allMagazines);
        }
        [Fact]
        public void GetMagazineById_ReturnsCorrectMagazine()
        {
            var magazineId = 1;
            var expectedMagazine = new Domain.DTOs.Magazine { MagazineID = magazineId, IssueNumber = "40/2023" };
            _magazineRepository.Setup(x => x.GetMagazineById(magazineId)).Returns(expectedMagazine);
            var magazineModel = new Models.MagazineModel(_logger, _magazineRepository.Object);

            var magazine = magazineModel.GetMagazineById(magazineId);

            _magazineRepository.Verify(x => x.GetMagazineById(magazineId), Times.Once());
            Assert.Equal(expectedMagazine, magazine);
        }
        [Fact]
        public void GetMagazineByIdResponse_ReturnsCorrectResponseDTO()
        {

            var magazineId = 1;
            var expectedMagazineDTO = new Domain.DTOs.Magazine { MagazineID = magazineId, IssueNumber = "40/2023" };
            _magazineRepository.Setup(x => x.GetMagazineByIdResponse(magazineId)).Returns(expectedMagazineDTO);
            var magazineModel = new Models.MagazineModel(_logger, _magazineRepository.Object);


            var magazineDTO = magazineModel.GetMagazineByIdResponse(magazineId);

            _magazineRepository.Verify(x => x.GetMagazineByIdResponse(magazineId), Times.Once());
            Assert.Equal(expectedMagazineDTO, magazineDTO);
        }
        [Fact]
        public void GetAllMagazinesResponse_ReturnsExpectedResult()
        {
            var magazineModel = new Models.MagazineModel(_logger, _magazineRepository.Object);
            var expectedMagazines = new List<Domain.DTOs.Magazine>
            {
                new Domain.DTOs.Magazine  { MagazineID = 1,  IssueNumber = "40/2023" },
                new Domain.DTOs.Magazine  {MagazineID = 1,  IssueNumber = "40/2023" },
                new Domain.DTOs.Magazine  { MagazineID = 1,  IssueNumber = "40/2023" }
            };

            _magazineRepository.Setup(repo => repo.GetAllMagazinesResponse())
                .Returns(expectedMagazines);

            var result = magazineModel.GetAllMagazinesResponse();

            Assert.Equal(expectedMagazines, result);
            _magazineRepository.Verify(x => x.GetAllMagazinesResponse(), Times.Once);
        }
        [Fact]
        public void AddCopyOfMagazineById_ReturnsAddedCopy()
        {
            var magazineModel = new Models.MagazineModel(_logger, _magazineRepository.Object);
            var magazineId = 1;
            var expectedCopyId = 2;
            var isAvailable = true;
            var expectedCopy = new Domain.DTOs.Copy { CopyID = expectedCopyId, ItemID = magazineId };
            _magazineRepository.Setup(x => x.AddCopyOfMagazineById(magazineId, isAvailable)).Returns(expectedCopy);

            var addedCopy = magazineModel.AddCopyOfMagazineById(magazineId, isAvailable);

            Assert.NotNull(addedCopy);
            Assert.Equal(expectedCopyId, addedCopy.CopyID);
            Assert.Equal(magazineId, addedCopy.ItemID);
            _magazineRepository.Verify(x => x.AddCopyOfMagazineById(magazineId, isAvailable), Times.Once);
        }
        [Fact]
        public void AddCopyOfMagazineByIdResponse_ReturnsAddedCopy()
        {
            var magazineModel = new Models.MagazineModel(_logger, _magazineRepository.Object);
            var magazineId = 1;
            var copy = new Domain.DTOs.Magazine { MagazineID = 1, IssueNumber = "40/2023" };
            _magazineRepository.Setup(repo => repo.AddCopyOfMagazineByIdResponse(magazineId)).Returns(copy);

            var addedCopy = magazineModel.AddCopyOfMagazineByIdResponse(magazineId);

            Assert.NotNull(addedCopy);
            Assert.Equal(copy.MagazineID, addedCopy.MagazineID);
            Assert.Equal(copy.IssueNumber, addedCopy.IssueNumber);
            _magazineRepository.Verify(x => x.AddCopyOfMagazineByIdResponse(magazineId), Times.Once);
        }
    }
}
