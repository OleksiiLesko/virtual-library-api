using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Models;
using VirtualLibraryAPI.Repository;
using VirtualLibraryAPI.Repository.Repositories;

namespace VirtualLibraryAPI.Tests
{
    public class ManagementModelTest
    {
        private readonly ILogger<Models.ManagementModel> _logger;
        private readonly Mock<IManagementRepository> _managementRepository;
        private readonly Mock<ManagementModel> _model;

        public ManagementModelTest()
        {
            _managementRepository = new Mock<IManagementRepository>();
            _logger = new Mock<ILogger<Models.ManagementModel>>().Object;
            _model = new Mock<ManagementModel>();
        }
        [Fact]
        public void ReserveCopyById_Should_Change_Status_And_ExpirationDate()
        {
            var copyId = 1;
            var bookingPeriod = 7;
            var copy = new Domain.DTOs.Copy { CopyID = copyId };
            _managementRepository.Setup(x => x.ReserveCopyById(copyId, bookingPeriod)).Returns(copy);
            var copyModel = new Models.ManagementModel(_logger, _managementRepository.Object);
            var expectedExpirationDate = DateTime.Now.Date.AddDays(bookingPeriod);

            var result = copyModel.ReserveCopyById(copyId, bookingPeriod);

            Assert.Equal(copy.CopyID, result.CopyID);
            Assert.False(result.IsAvailable);
            _managementRepository.Verify(x => x.ReserveCopyById(copyId, bookingPeriod), Times.Once());
        }
        [Fact]
        public void ReturnCopyById_Should_Change_Status_And_ExpirationDate()
        {
            var copyId = 1;
            var returnCopy = new Domain.DTOs.Copy { CopyID = 2};
            _managementRepository.Setup(x => x.ReturnCopyById(copyId)).Returns(returnCopy);

            var copyModel = new Models.ManagementModel(_logger, _managementRepository.Object);

            var result = copyModel.ReturnCopyById(copyId);

            Assert.Equal(returnCopy.CopyID, result.CopyID);
            Assert.True(result.IsAvailable = true);
            Assert.Equal(DateTime.MinValue, result.ExpirationDate);
            _managementRepository.Verify(x => x.ReturnCopyById(copyId), Times.Once());
        }
        [Fact]
        public void ReserveCopyById_ReturnsReservedCopy()
        {
            int copyId = 1;
            int bookingPeriod = 7;
            var expectedCopy = new Domain.DTOs.Copy
            {
                CopyID = copyId,
                IsAvailable = false,
                ExpirationDate = DateTime.Now.AddDays(bookingPeriod)
            };
            _managementRepository.Setup(m => m.ReserveCopyById(copyId, bookingPeriod)).Returns(expectedCopy);
            var managementModel = new Models.ManagementModel(_logger, _managementRepository.Object);

            var result = managementModel.ReserveCopyById(copyId, bookingPeriod);

            Assert.Equal(expectedCopy, result);
            Assert.False(result.IsAvailable);
            Assert.Equal(expectedCopy.ExpirationDate.Date, result.ExpirationDate.Date);
        }

        [Fact]
        public void ReserveCopyById_ReturnsNull_WhenCopyNotReserved()
        {
            int copyId = 1;
            int bookingPeriod = 7;
            _managementRepository.Setup(m => m.ReserveCopyById(copyId, bookingPeriod)).Returns((Domain.DTOs.Copy)null);
            var managementModel = new Models.ManagementModel(_logger, _managementRepository.Object);

            var result = managementModel.ReserveCopyById(copyId, bookingPeriod);

            Assert.Null(result);
        }
        [Fact]
        public void ReturnCopyById_ReturnsCopyWithUpdatedAvailabilityAndExpirationDate()
        {
            int copyId = 1;
            var expectedCopy = new Domain.DTOs.Copy
            {
                CopyID = copyId,
                IsAvailable = true,
                ExpirationDate = DateTime.MinValue
            };

            _managementRepository.Setup(m => m.ReturnCopyById(copyId)).Returns(expectedCopy);

            var managementModel = new Models.ManagementModel(_logger, _managementRepository.Object);

            var result = managementModel.ReturnCopyById(copyId);

            Assert.NotNull(result);
            Assert.Equal(expectedCopy.CopyID, result.CopyID);
            Assert.True(result.IsAvailable);
            Assert.Equal(DateTime.MinValue, result.ExpirationDate);
        }
        [Fact]
        public void GetAllExpiredItems_ReturnsItems()
        {
            var expectedItems = new List<Domain.DTOs.Item> { new Domain.DTOs.Item(), new Domain.DTOs.Item() };
            _managementRepository.Setup(repo => repo.GetAllExpiredItems()).Returns(expectedItems);
            var managementModel = new Models.ManagementModel(_logger, _managementRepository.Object);

            var actualItems = managementModel.GetAllExpiredItems();

            Assert.Equal(expectedItems, actualItems);
        }

        [Fact]
        public void GetAllExpiredItemsResponse_ReturnsResult()
        {
            var expectedItems = new List<Domain.DTOs.Copy> { new Domain.DTOs.Copy(), new Domain.DTOs.Copy() };
            _managementRepository.Setup(repo => repo.GetAllExpiredItemsResponse()).Returns(expectedItems);

            var managementModel = new Models.ManagementModel(_logger, _managementRepository.Object);
            var actualItems = managementModel.GetAllExpiredItemsResponse();

            Assert.Equal(expectedItems, actualItems);
        }

        [Fact]
        public void GetAllExpiredItemsResponse_ReturnsNull()
        {
            _managementRepository.Setup(repo => repo.GetAllExpiredItemsResponse()).Returns((IEnumerable<Domain.DTOs.Copy>)null);
            var managementModel = new Models.ManagementModel(_logger, _managementRepository.Object);

            var result = managementModel.GetAllExpiredItemsResponse();

            Assert.Null(result);
        }
    }
}
