﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;
using VirtualLibraryAPI.Library.Controllers;
using VirtualLibraryAPI.Domain.DTOs;
using VirtualLibraryAPI.Common;
using VirtualLibraryAPI.Models;
using VirtualLibraryAPI.Repository;
using FluentValidation;

namespace VirtualLibraryAPI.Tests
{
    public class ManagementControllerTest
    {
        private readonly Mock<ILogger<ManagementController>> _loggerMock;
        private readonly Mock<IManagementModel> _managementModelMock;
        private readonly Mock<IValidator<Copy>> _validationCopyModel;
        private readonly Mock<IValidationClientModel> _validationUserModel;
        private readonly Mock<IValidationIssuerModel> _validationIssuerModel;
        private readonly ManagementController _managementController;

        public ManagementControllerTest()
        {
            _loggerMock = new Mock<ILogger<ManagementController>>();
            _managementModelMock = new Mock<IManagementModel>();
            _validationIssuerModel = new Mock<IValidationIssuerModel>();
            _validationUserModel = new Mock<IValidationClientModel>();
            _validationCopyModel = new Mock<IValidator<Copy>>();
            _managementController = new ManagementController(_loggerMock.Object, _managementModelMock.Object, _validationCopyModel.Object, _validationUserModel.Object, _validationIssuerModel.Object);
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
        public void ReserveCopyById_InvalidBookingPeriod_ReturnsBadRequest()
        {
            var copyId = 1;
            var bookingPeriod = 7;
            var clientID = 1;
            var adminID = 2;

            var result = _managementController.ReserveCopyById(adminID, clientID, copyId, bookingPeriod);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void GetAllExpiredItems_ReturnsOkResult()
        {
            var expectedItems = new List<Item> { new Item(), new Item() };
            _managementModelMock.Setup(repo => repo.GetAllExpiredItems()).Returns(expectedItems);

            IActionResult result = _managementController.GetAllExpiredItems();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetAllExpiredItems_ReturnsNotFound()
        {
            _managementModelMock.Setup(repo => repo.GetAllExpiredItems()).Returns((IEnumerable<Item>)null);


            IActionResult result = _managementController.GetAllExpiredItems();

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetAllExpiredItems_ThrowsException_ReturnsBadRequest()
        {
            _managementModelMock.Setup(m => m.GetAllExpiredItems()).Throws(new Exception("Some error"));

            IActionResult result = _managementController.GetAllExpiredItems();

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}