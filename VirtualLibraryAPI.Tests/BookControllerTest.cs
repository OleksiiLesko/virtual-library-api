using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using VirtualLibraryAPI.Common;
using VirtualLibraryAPI.Domain.Entities;
using VirtualLibraryAPI.Library.Controllers;
using VirtualLibraryAPI.Models;
using VirtualLibraryAPI.Repository;
using VirtualLibraryAPI.Repository.Repositories;
using Xunit;
using UserType = VirtualLibraryAPI.Common.UserType;

namespace VirtualLibraryAPI.Tests
{
    public class BookControllerTests
    {
        private readonly Mock<ILogger<BookController>> _logger;
        private readonly Mock<IBookModel> _bookModel;
        private readonly Mock<IDepartmentModel> _departmentModel;
        private readonly BookController _controller;

        public BookControllerTests()
        {
            _logger = new Mock<ILogger<BookController>>();
            _bookModel = new Mock<IBookModel>();
            _departmentModel = new Mock<IDepartmentModel>();
            _controller = new BookController(_logger.Object, _bookModel.Object, _departmentModel.Object);
        }


        [Fact]
        public void GettAllBooks_ReturnOK()
        {
            var books = new List<Domain.DTOs.Book>
        {
            new Domain.DTOs.Book { BookID = 1, Author = "Book 1" },
            new Domain.DTOs.Book { BookID = 2, Author = "Book 2" },
            new Domain.DTOs.Book { BookID = 3, Author = "Book 3" }
        };
            _bookModel.Setup(model => model.GetAllBooks()).Returns(books);


            var result = _controller.GetAllBooks();

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void GettAllBooks_ReturnNotFound()
        {
            List<Domain.DTOs.Book> books = null;
            _bookModel.Setup(m => m.GetAllBooks()).Returns(books);

            var result = _controller.GetAllBooks();

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void GettAllBooks_ReturnbBadRequest()
        {
            _bookModel.Setup(model => model.GetAllBooks()).Throws(new Exception("Failed to retrieve books"));

            var result = _controller.GetAllBooks();

            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed", badRequestResult.Value);
        }
        [Fact]
        public void AddBook_ReturnOK()
        {
            var request = new Domain.DTOs.Book();
            var userType = UserType.Administrator;

            _departmentModel.Setup(m => m.GetDepartmentById(It.IsAny<int>())).Returns(new Domain.DTOs.Department { });

            _bookModel.Setup(m => m.AddBook(It.IsAny<Domain.DTOs.Book>())).Returns(new Domain.DTOs.Book { });

            var result = _controller.AddBook(request);

            var okResult = Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void AddBook_ReturnNotFound()
        {
            var request = new Domain.DTOs.Book();
            var userType = UserType.Administrator;

            _departmentModel.Setup(m => m.GetDepartmentById(It.IsAny<int>())).Returns(new Domain.DTOs.Department { });

            _bookModel.Setup(m => m.AddBook(It.IsAny<Domain.DTOs.Book>())).Returns((Domain.DTOs.Book)null);

            var result = _controller.AddBook(request);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void AddBook_ReturnBadRequest()
        {
            var request = new Domain.DTOs.Book
            {
                Name = "Book Name",
                PublishingDate = DateTime.Now,
                Publisher = "Publisher"
            };

            _bookModel.Setup(model => model.AddBook(request)).Throws(new ArgumentException());

            var result = _controller.AddBook(request);

            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void AddCopyOfBookById_ReturnOK()
        {
            var bookId = 1;
            var isAvailable = true;
            var addedBook = new Domain.DTOs.Copy
            {
                ItemID = 2,
                CopyID = bookId
            };

            _bookModel.Setup(model => model.AddCopyOfBookById(bookId, isAvailable)).Returns(addedBook);

            var result = _controller.AddCopyOfBookById(bookId);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void AddCopyOfBookById_ReturnNotFound()
        {
            var bookId = 1;
            var isAvailable = true;
            _bookModel.Setup(model => model.AddCopyOfBookById(bookId, isAvailable)).Returns((Domain.DTOs.Copy)null);

            var result = _controller.AddCopyOfBookById(bookId);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void AddCopyOfBookById_ReturnbBadRequest()
        {
            var bookId = 1;
            var isAvailable = true;
            _bookModel.Setup(model => model.AddCopyOfBookById(bookId, isAvailable)).Throws(new Exception("Some error message"));


            var result = _controller.AddCopyOfBookById(bookId);

            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed", badRequestResult.Value);
        }
        [Fact]
        public void GetBookById_ReturnOK()
        {

            var bookId = 1;
            var expectedBook = new Domain.DTOs.Book
            {
                BookID = bookId,
                Author = "Author",
                ISBN = "ISBN"
            };

            _bookModel.Setup(model => model.GetBookById(bookId)).Returns(expectedBook);


            var result = _controller.GetBookById(bookId);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void GetBookById_ReturnNotFound()
        {
            var bookId = 1;
            _bookModel.Setup(model => model.GetBookById(bookId)).Returns(null as Domain.DTOs.Book);

            var result = _controller.GetBookById(bookId);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void GetBookById_ReturnbBadRequest()
        {
            var bookId = 1;
            _bookModel.Setup(model => model.GetBookById(bookId)).Throws(new Exception("Error retrieving book"));

            var result = _controller.GetBookById(bookId);

            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void UpdateBook_ReturnOK()
        {
            int Id = 123;
            var request = new Domain.DTOs.Book();
            var userType = UserType.Administrator;

            _departmentModel.Setup(m => m.GetDepartmentById(It.IsAny<int>())).Returns(new Domain.DTOs.Department { });

            _bookModel.Setup(m => m.UpdateBook(It.IsAny<int>(), It.IsAny<Domain.DTOs.Book>())).Returns(new Domain.DTOs.Book { });

            var result = _controller.UpdateBook(Id, request);

            var okResult = Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void UpdateBook_ReturnNotFound()
        {
            int userId = 123;
            var request = new Domain.DTOs.Book();
            var userType = UserType.Administrator;

            _departmentModel.Setup(m => m.GetDepartmentById(It.IsAny<int>())).Returns(new Domain.DTOs.Department { });

            _bookModel.Setup(m => m.UpdateBook(It.IsAny<int>(), It.IsAny<Domain.DTOs.Book>())).Returns((Domain.DTOs.Book)null);

            var result = _controller.UpdateBook(userId, request);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void UpdateBook_ReturnbBadRequest()
        {
            var bookId = 1;
            var request = new Domain.DTOs.Book
            {
                Name = "Updated Book Name",
                PublishingDate = DateTime.Now,
                Publisher = "Updated Publisher"
            };

            _bookModel.Setup(model => model.UpdateBook(bookId, request)).Throws(new Exception("Update failed"));

            var result = _controller.UpdateBook(bookId, request);

            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void DeleteBook_ReturnNoContent()
        {
            var bookId = 1;
            _bookModel.Setup(model => model.GetBookById(bookId)).Returns(new Domain.DTOs.Book());

            var result = _controller.DeleteBook(bookId);

            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public void DeleteBook_ReturnNotFound()
        {
            var bookId = 1;
            _bookModel.Setup(model => model.GetBookById(bookId)).Returns((Domain.DTOs.Book)null);

            var result = _controller.DeleteBook(bookId);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void DeleteBook_ReturnsBadRequest_WhenExceptionThrown()
        {
            var bookId = 1;
            _bookModel.Setup(model => model.GetBookById(bookId)).Throws<Exception>();

            var result = _controller.DeleteBook(bookId);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
