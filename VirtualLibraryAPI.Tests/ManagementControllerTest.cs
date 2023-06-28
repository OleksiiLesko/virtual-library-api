using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;
using VirtualLibraryAPI.Library.Controllers;
using VirtualLibraryAPI.Domain.DTOs;
using VirtualLibraryAPI.Common;
using VirtualLibraryAPI.Models;
using VirtualLibraryAPI.Repository;

namespace VirtualLibraryAPI.Tests
{
    public class ManagementControllerTest
    {
        private readonly Mock<ILogger<ManagementController>> _loggerMock;
        private readonly Mock<IManagementModel> _managementModelMock;
        private readonly Mock<IValidationModel> _validationModelMock;
        private readonly ManagementController _managementController;

        public ManagementControllerTest()
        {
            _loggerMock = new Mock<ILogger<ManagementController>>();
            _managementModelMock = new Mock<IManagementModel>();
            _validationModelMock = new Mock<IValidationModel>();
            _managementController = new ManagementController(_loggerMock.Object, _managementModelMock.Object, _validationModelMock.Object);
        }

        [Fact]
        public void ReturnCopyById_ReturnsOk()
        {
            int copyId = 1;
            _managementModelMock.Setup(repo => repo.ReturnCopyById(copyId)).Returns(new Domain.DTOs.Copy());

            IActionResult result = _managementController.ReturnCopyById(copyId);

            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal("Copy returned", okResult.Value);
        }

        [Fact]
        public void ReturnCopyById_ReturnsNotFound()
        {
            int copyId = 1;
            _managementModelMock.Setup(repo => repo.ReturnCopyById(copyId)).Returns((Copy)null);

            IActionResult result = _managementController.ReturnCopyById(copyId);


            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void ReturnCopyById_ThrowsException_ReturnsBadRequest()
        {
            var copyId = 1;
            _managementModelMock.Setup(m => m.ReturnCopyById(copyId)).Throws(new Exception("Some error"));

            var result = _managementController.ReturnCopyById(copyId);

 
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void ReserveCopyById_ValidCopy_ReturnsOkResult()
        {
            var copyId = 1;
            var bookingPeriod = 7;

            _validationModelMock.Setup(v => v.IsCopyValidForBooking(copyId, bookingPeriod))
                .Returns(ValidationStatus.Valid);

            var reservedCopy = new Domain.DTOs.Copy
            {
                CopyID = copyId,
                ExpirationDate = DateTime.Now.AddDays(bookingPeriod)
            };
            _managementModelMock.Setup(m => m.ReserveCopyById(copyId, bookingPeriod))
                .Returns(reservedCopy);

            var result = _managementController.ReserveCopyById(copyId, bookingPeriod);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var bookingCopy = Assert.IsType<BookingCopy>(okResult.Value);

            Assert.Equal(copyId, bookingCopy.CopyID);
            Assert.Equal(reservedCopy.ExpirationDate, bookingCopy.ExpirationDate);
        }
        [Fact]
        public void ReserveCopyById_InvalidCopy_ReturnsBadRequest()
        {
            var copyId = 1;
            var bookingPeriod = 7;

            _validationModelMock.Setup(v => v.IsCopyValidForBooking(copyId, bookingPeriod))
                .Returns(ValidationStatus.InvalidBookingPeriod);

            var result = _managementController.ReserveCopyById(copyId, bookingPeriod);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}