using Castle.Core.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Common;
using VirtualLibraryAPI.Domain.Entities;
using VirtualLibraryAPI.Models;
using VirtualLibraryAPI.Repository;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace VirtualLibraryAPI.Tests
{
    public class ValidationCopyModelTest
    {
        [Fact]
        public void IsCopyValidForBooking_ValidCopy_ReturnsValidStatus()
        {
            var copyId = 1;
            var requestedBookingPeriod = 7;
            var mockConfiguration = new Mock<IConfiguration>();
            var mockRepository = new Mock<ICopyRepository>();
            var mockLogger = new Mock<ILogger<ValidationCopyModel>>();
            var validationModel = new ValidationCopyModel(mockLogger.Object, mockConfiguration.Object, mockRepository.Object);

            var copy = new Domain.DTOs.Copy { CopyID = copyId, IsAvailable = true, Type = Domain.DTOs.Type.Book };
            mockRepository.Setup(r => r.GetCopyById(copyId)).Returns(copy);
            mockConfiguration.Setup(c => c["MaxDays:Book"]).Returns("30");

            var result = validationModel.IsCopyValidForBooking(copyId, requestedBookingPeriod);

            Assert.Equal(ValidationCopyStatus.Valid, result);
        }


        [Fact]
        public void IsCopyValidForBooking_CopyNotFound_ReturnsNotFoundStatus()
        {
            var copyId = 1;
            var requestedBookingPeriod = 7;
            var mockConfiguration = new Mock<IConfiguration>();
            var mockRepository = new Mock<ICopyRepository>();
            var mockLogger = new Mock<ILogger<ValidationCopyModel>>();
            var validationModel = new ValidationCopyModel(mockLogger.Object, mockConfiguration.Object, mockRepository.Object);

            mockRepository.Setup(r => r.GetCopyById(copyId)).Returns((Domain.DTOs.Copy)null);

            var result = validationModel.IsCopyValidForBooking(copyId, requestedBookingPeriod);

            Assert.Equal(ValidationCopyStatus.NotFound, result);
        }

        [Fact]
        public void IsCopyValidForBooking_CopyNotAvailable_ReturnsNotAvailableStatus()
        {
            var copyId = 1;
            var requestedBookingPeriod = 7;
            var mockConfiguration = new Mock<IConfiguration>();
            var mockRepository = new Mock<ICopyRepository>();
            var mockLogger = new Mock<ILogger<ValidationCopyModel>>();
            var validationModel = new ValidationCopyModel(mockLogger.Object, mockConfiguration.Object, mockRepository.Object);

            mockRepository.Setup(r => r.GetCopyById(copyId)).Returns(new Domain.DTOs.Copy { CopyID = copyId, IsAvailable = false, Type = Domain.DTOs.Type.Book });

            var result = validationModel.IsCopyValidForBooking(copyId, requestedBookingPeriod);

            Assert.Equal(ValidationCopyStatus.NotAvailable, result);
        }

        [Fact]
        public void IsCopyValidForBooking_InvalidBookingPeriod_ReturnsInvalidBookingPeriodStatus()
        {
            var copyId = 1;
            var requestedBookingPeriod = -1;
            var mockConfiguration = new Mock<IConfiguration>();
            var mockRepository = new Mock<ICopyRepository>();
            var mockLogger = new Mock<ILogger<ValidationCopyModel>>();
            var validationModel = new ValidationCopyModel(mockLogger.Object, mockConfiguration.Object, mockRepository.Object);

            mockRepository.Setup(r => r.GetCopyById(copyId)).Returns(new Domain.DTOs.Copy { CopyID = copyId, IsAvailable = true, Type = Domain.DTOs.Type.Book });

            var result = validationModel.IsCopyValidForBooking(copyId, requestedBookingPeriod);

            Assert.Equal(ValidationCopyStatus.InvalidBookingPeriod, result);
        }

        [Fact]
        public void IsCopyValidForBooking_DbException_ReturnsDbErrorStatus()
        {
            var copyId = 1;
            var requestedBookingPeriod = 7;
            var mockConfiguration = new Mock<IConfiguration>();
            var mockRepository = new Mock<ICopyRepository>();
            var mockLogger = new Mock<ILogger<ValidationCopyModel>>();
            var validationModel = new ValidationCopyModel(mockLogger.Object, mockConfiguration.Object, mockRepository.Object);
            var dbExceptionMock = new Mock<DbException>();

            mockRepository.Setup(c => c.GetCopyById(copyId)).Throws(dbExceptionMock.Object);

            var result = validationModel.IsCopyValidForBooking(copyId, requestedBookingPeriod);

            Assert.Equal(ValidationCopyStatus.DbError, result);
        }

        [Fact]
        public void IsCopyValidForBooking_GenericException_ReturnsInternalServerErrorStatus()
        {
            var copyId = 1;
            var requestedBookingPeriod = 7;
            var mockConfiguration = new Mock<IConfiguration>();
            var mockRepository = new Mock<ICopyRepository>();
            var mockLogger = new Mock<ILogger<ValidationCopyModel>>();
            var validationModel = new ValidationCopyModel(mockLogger.Object, mockConfiguration.Object, mockRepository.Object);

            mockRepository.Setup(r => r.GetCopyById(copyId)).Throws(new Exception());

            var result = validationModel.IsCopyValidForBooking(copyId, requestedBookingPeriod);

            Assert.Equal(ValidationCopyStatus.InternalServerError, result);
        }
    }
}
