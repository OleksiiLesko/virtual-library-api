using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using VirtualLibraryAPI.Domain.Entities;
using VirtualLibraryAPI.Library.Controllers;
using VirtualLibraryAPI.Models;
using VirtualLibraryAPI.Repository;
using VirtualLibraryAPI.Repository.Repositories;
using Xunit;

namespace VirtualLibraryAPI.Tests
{
    public class BookControllerTests
    {
        private readonly Mock<ILogger<BookController>> _loggerMock;
        private readonly Mock<ILogger<Models.Book>> _loggerBook;
        private readonly Mock<IBook> _bookRepository;
        private readonly Models.Book _bookModelMock;
        private readonly BookController _bookController;

        public BookControllerTests()
        {
            _loggerMock = new Mock<ILogger<BookController>>();
            _loggerBook = new Mock<ILogger<Models.Book>>();
            _bookRepository = new Mock<IBook>();
            _bookModelMock = new Models.Book(_bookRepository.Object, _loggerBook.Object);
            _bookController = new BookController(_loggerMock.Object, _bookModelMock);
        }


        [Fact]
        public void GettAllBooks_ReturnOK()
        {
            var books = new List<Domain.Entities.Book>
        {
            new Domain.Entities.Book { ItemID = 1, Author = "Book 1" },
            new Domain.Entities.Book { ItemID = 2, Author = "Book 2" },
            new Domain.Entities.Book { ItemID = 3, Author = "Book 3" }
        };
            _bookRepository.Setup(model => model.GetAllBooks()).Returns(books);


            var result = _bookController.GetAllBooks();

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void GettAllBooks_ReturnNotFound()
        {
            _bookRepository.Setup(model => model.GetAllBooks()).Returns((List<Domain.Entities.Book>)null);

            var result = _bookController.GetAllBooks();

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void GettAllBooks_ReturnbBadRequest()
        {
            _bookRepository.Setup(model => model.GetAllBooks()).Throws(new Exception("Failed to retrieve books"));

            var result = _bookController.GetAllBooks();

            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed", badRequestResult.Value);
        }
        [Fact]
        public void AddBook_ReturnOK()
        {
            var request = new Domain.DTOs.Book
            {
                Name = "Book Name",
                PublishingDate = DateTime.Now,
                Publisher = "Publisher"
            };

            var addedBook = new Domain.Entities.Book
            {
                ItemID = 1,
                ISBN = "23324",
                Author = "Arnold"
            };

            _bookRepository.Setup(model => model.AddBook(request)).Returns(addedBook);

            var result = _bookController.AddBook(request);

            Assert.IsType<OkObjectResult>(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var bookResponse = Assert.IsType<Domain.DTOs.Book>(okResult.Value);

            Assert.Equal(addedBook.ItemID, bookResponse.BookID);
            Assert.Equal(request.Name, bookResponse.Name);
            Assert.Equal(request.PublishingDate, bookResponse.PublishingDate);
            Assert.Equal(request.Publisher, bookResponse.Publisher);
            Assert.Equal(addedBook.ISBN, bookResponse.ISBN);
            Assert.Equal(addedBook.Author, bookResponse.Author);
        }
        [Fact]
        public void AddBook_ReturnNotFound()
        {
            var request = new Domain.DTOs.Book
            {
                Name = "Book Name",
                PublishingDate = DateTime.Now,
                Publisher = "Publisher"
            };

            _bookRepository.Setup(model => model.AddBook(request)).Returns((Domain.Entities.Book)null);

            var result = _bookController.AddBook(request);

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

            _bookRepository.Setup(model => model.AddBook(request)).Throws(new ArgumentException());

            var result = _bookController.AddBook(request);

            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void AddCopyOfBookById_ReturnOK()
        {
            var bookId = 1;
            var addedBook = new Domain.Entities.Copy
            {
                ItemID = 2,
                CopyID = bookId
            };

            _bookRepository.Setup(model => model.AddCopyOfBookById(bookId)).Returns(addedBook);

            var result = _bookController.AddCopyOfBookById(bookId);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void AddCopyOfBookById_ReturnNotFound()
        {
            var bookId = 1;
            _bookRepository.Setup(model => model.AddCopyOfBookById(bookId)).Returns((Domain.Entities.Copy)null);

            var result = _bookController.AddCopyOfBookById(bookId);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void AddCopyOfBookById_ReturnbBadRequest()
        {
            var bookId = 1;
            _bookRepository.Setup(model => model.AddCopyOfBookById(bookId)).Throws(new Exception("Some error message"));


            var result = _bookController.AddCopyOfBookById(bookId);

            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed", badRequestResult.Value);
        }
        [Fact]
        public void GetBookById_ReturnOK()
        {

            var bookId = 1;
            var expectedBook = new Domain.Entities.Book
            {
                ItemID = bookId,
                Author = "Author",
                ISBN = "ISBN"
            };

            _bookRepository.Setup(model => model.GetBookById(bookId)).Returns(expectedBook);


            var result = _bookController.GetBookById(bookId);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void GetBookById_ReturnNotFound()
        {
            var bookId = 1;
            _bookRepository.Setup(model => model.GetBookById(bookId)).Returns(null as Domain.Entities.Book);

            var result = _bookController.GetBookById(bookId);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void GetBookById_ReturnbBadRequest()
        {
            var bookId = 1;
            _bookRepository.Setup(model => model.GetBookById(bookId)).Throws(new Exception("Error retrieving book"));

            var result = _bookController.GetBookById(bookId);

            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void UpdateBook_ReturnOK()
        {
            var bookId = 1;
            var request = new Domain.DTOs.Book
            {
                Name = "Updated Book Name",
                PublishingDate = DateTime.Now,
                Publisher = "Updated Publisher"
            };
            var updatedBook = new Domain.Entities.Book
            {
                ItemID = bookId,
                Author = "Author",
                ISBN = "ISBN"
            };

            _bookRepository.Setup(model => model.UpdateBook(bookId, request)).Returns(updatedBook);

            var result = _bookController.UpdateBook(bookId, request);

            Assert.IsType<OkObjectResult>(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var bookResponse = Assert.IsType<Domain.DTOs.Book>(okResult.Value);

            Assert.Equal(updatedBook.ItemID, bookResponse.BookID);
            Assert.Equal(request.Name, bookResponse.Name);
            Assert.Equal(updatedBook.Author, bookResponse.Author);
            Assert.Equal(updatedBook.ISBN, bookResponse.ISBN);
            Assert.Equal(request.Publisher, bookResponse.Publisher);
            Assert.Equal(request.PublishingDate, bookResponse.PublishingDate);
        }
        [Fact]
        public void UpdateBook_ReturnNotFound()
        {
            var bookId = 1;
            var request = new Domain.DTOs.Book
            {
                Name = "Updated Book Name",
                PublishingDate = DateTime.Now,
                Publisher = "Updated Publisher"
            };

            _bookRepository.Setup(model => model.UpdateBook(bookId, request)).Returns((Domain.Entities.Book)null);

            var result = _bookController.UpdateBook(bookId, request);

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

            _bookRepository.Setup(model => model.UpdateBook(bookId, request)).Throws(new Exception("Update failed"));

            var result = _bookController.UpdateBook(bookId, request);

            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void DeleteBook_ReturnNoContent()
        {
            var bookId = 1;
            _bookRepository.Setup(model => model.GetBookById(bookId)).Returns(new Domain.Entities.Book());

            var result = _bookController.DeleteBook(bookId);

            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public void DeleteBook_ReturnNotFound()
        {
            var bookId = 1;
            _bookRepository.Setup(model => model.GetBookById(bookId)).Returns((Domain.Entities.Book)null);

            var result = _bookController.DeleteBook(bookId);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void DeleteBook_ReturnsBadRequest_WhenExceptionThrown()
        {
            var bookId = 1;
            _bookRepository.Setup(model => model.GetBookById(bookId)).Throws<Exception>();

            var result = _bookController.DeleteBook(bookId);

            Assert.IsType<BadRequestObjectResult>(result);
        }

    }
}
