using Microsoft.Extensions.Logging;
using Moq;
using VirtualLibraryAPI.Repository;

namespace VirtualLibraryAPI.Tests
{
    public class BookModelTest
    {
        private readonly ILogger<Models.BookModel> _logger;
        private readonly Mock<IBookRepository> _bookRepository;

        public BookModelTest()
        {
            _bookRepository = new Mock<IBookRepository>();
            _logger = new Mock<ILogger<Models.BookModel>>().Object;
        }

        [Fact]
        public void AddBook_ReturnsAddedBook()
        {
            var bookDto = new Domain.DTOs.Book { BookID = 2, ISBN = "324235", Author = "Test Author" };
            var addedBook = new Domain.DTOs.Book { BookID = 1, ISBN = "324235", Author = "Test Author" };
            _bookRepository.Setup(x => x.AddBook(bookDto)).Returns(addedBook);
            var bookModel = new Models.BookModel(_logger, _bookRepository.Object);

            var result = bookModel.AddBook(bookDto);

            Assert.NotNull(result);
            Assert.Equal(bookDto.ISBN, result.ISBN);
            Assert.Equal(bookDto.Author, result.Author);
            Assert.NotEqual(bookDto.BookID, result.BookID);
            _bookRepository.Verify(x => x.AddBook(bookDto), Times.Once());
        }
        [Fact]
        public void UpdateBook_Should_Return_Updated_Book()
        {
            var bookID = 1;
            var bookDto = new Domain.DTOs.Book { BookID = 2, ISBN = "324235", Author = "Test Author" };
            var updatedBook = new Domain.DTOs.Book { BookID = bookID, ISBN = "324235", Author = "Test Author" };
            _bookRepository.Setup(x => x.UpdateBook(bookID, bookDto)).Returns(updatedBook);
            var bookModel = new Models.BookModel(_logger, _bookRepository.Object);

            var result = bookModel.UpdateBook(bookID, bookDto);

            Assert.True(result.BookID == 1);
            Assert.Equal(updatedBook.BookID, result.BookID);
            Assert.Equal(updatedBook.ISBN, result.ISBN);
            Assert.Equal(updatedBook.Author, result.Author);
            _bookRepository.Verify(x => x.UpdateBook(bookID, bookDto), Times.Once());
        }
        [Fact]
        public void DeleteBook_ReturnsDeletedBook()
        {
            int bookIdToDelete = 1;
            var expectedDeletedBook = new Domain.DTOs.Book { BookID = bookIdToDelete, ISBN = "1234567890", Author = "Test Author" };
            _bookRepository.Setup(x => x.DeleteBook(bookIdToDelete)).Returns(expectedDeletedBook);
            var bookModel = new Models.BookModel(_logger, _bookRepository.Object);

            var deletedBook = bookModel.DeleteBook(bookIdToDelete);

            _bookRepository.Verify(x => x.DeleteBook(bookIdToDelete), Times.Once());
            Assert.Equal(expectedDeletedBook, deletedBook);
        }
        [Fact]
        public void GetAllBooks_ReturnsAllBooks()
        {
            var expectedBooks = new List<Domain.DTOs.Book>
        {
            new Domain.DTOs.Book { BookID = 1, ISBN = "1234567890", Author = "Test Author 1" },
            new Domain.DTOs.Book { BookID = 2, ISBN = "0987654321", Author = "Test Author 2" },
            new Domain.DTOs.Book { BookID = 3, ISBN = "5555555555", Author = "Test Author 3" }
        };
            _bookRepository.Setup(x => x.GetAllBooks()).Returns(expectedBooks);
            var bookModel = new Models.BookModel(_logger, _bookRepository.Object);

            var allBooks = bookModel.GetAllBooks();

            _bookRepository.Verify(x => x.GetAllBooks(), Times.Once());
            Assert.Equal(expectedBooks, allBooks);
        }
        [Fact]
        public void GetBookById_ReturnsCorrectBook()
        {
            var bookId = 1;
            var expectedBook = new Domain.DTOs.Book { BookID = bookId, ISBN = "1234567890", Author = "Test Author" };
            _bookRepository.Setup(x => x.GetBookById(bookId)).Returns(expectedBook);
            var bookModel = new Models.BookModel(_logger, _bookRepository.Object);

            var book = bookModel.GetBookById(bookId);

            _bookRepository.Verify(x => x.GetBookById(bookId), Times.Once());
            Assert.Equal(expectedBook, book);
        }
        [Fact]
        public void GetBookByIdResponse_ReturnsCorrectResponseDTO()
        {

            var bookId = 1;
            var expectedBookDTO = new Domain.DTOs.Book { BookID = bookId, ISBN = "1234567890", Author = "Test Author" };
            _bookRepository.Setup(x => x.GetBookByIdResponse(bookId)).Returns(expectedBookDTO);
            var bookModel = new Models.BookModel(_logger, _bookRepository.Object);


            var bookDTO = bookModel.GetBookByIdResponse(bookId);

            _bookRepository.Verify(x => x.GetBookByIdResponse(bookId), Times.Once());
            Assert.Equal(expectedBookDTO, bookDTO);
        }
        [Fact]
        public void GetAllBooksResponse_ReturnsExpectedResult()
        {
            var bookModel = new Models.BookModel(_logger, _bookRepository.Object);
            var expectedBooks = new List<Domain.DTOs.Book>
            {
                new Domain.DTOs.Book  { BookID = 1, ISBN = "1234567890", Author = "Author 1" },
                new Domain.DTOs.Book  { BookID = 2, ISBN = "0987654321", Author = "Author 2" },
                new Domain.DTOs.Book  { BookID = 3, ISBN = "1111111111", Author = "Author 3" }
            };

            _bookRepository.Setup(repo => repo.GetAllBooksResponse())
                .Returns(expectedBooks);

            var result = bookModel.GetAllBooksResponse();

            Assert.Equal(expectedBooks, result);
            _bookRepository.Verify(x => x.GetAllBooksResponse(), Times.Once);
        }
        [Fact]
        public void AddCopyOfBookById_ReturnsAddedCopy()
        {
            var bookModel = new Models.BookModel(_logger, _bookRepository.Object);
            var bookId = 1;
            var expectedCopyId = 2;
            var isAvailable = true;
            var expectedCopy = new Domain.DTOs.Copy { CopyID = expectedCopyId, ItemID = bookId };
            _bookRepository.Setup(x => x.AddCopyOfBookById(bookId, isAvailable)).Returns(expectedCopy);

            var addedCopy = bookModel.AddCopyOfBookById(bookId, isAvailable);

            Assert.NotNull(addedCopy);
            Assert.Equal(expectedCopyId, addedCopy.CopyID);
            Assert.Equal(bookId, addedCopy.ItemID);
            _bookRepository.Verify(x => x.AddCopyOfBookById(bookId, isAvailable), Times.Once);
        }
        [Fact]
        public void AddCopyOfBookByIdResponse_ReturnsAddedCopy()
        {
            var bookModel = new Models.BookModel(_logger, _bookRepository.Object);
            var bookId = 1;
            var copy = new Domain.DTOs.Book { BookID = bookId, ISBN = "123456", Author = "Test Author" };
            _bookRepository.Setup(repo => repo.AddCopyOfBookByIdResponse(bookId)).Returns(copy);

            var addedCopy = bookModel.AddCopyOfBookByIdResponse(bookId);

            Assert.NotNull(addedCopy);
            Assert.Equal(copy.BookID, addedCopy.BookID);
            Assert.Equal(copy.ISBN, addedCopy.ISBN);
            Assert.Equal(copy.Author, addedCopy.Author);
            _bookRepository.Verify(x => x.AddCopyOfBookByIdResponse(bookId), Times.Once);
        }
    }
}
