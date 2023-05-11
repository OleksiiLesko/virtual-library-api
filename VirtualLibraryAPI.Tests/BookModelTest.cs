using Microsoft.Extensions.Logging;
using Moq;
using VirtualLibraryAPI.Repository;

namespace VirtualLibraryAPI.Tests
{
    public class BookModelTest
    {
        private readonly ILogger<Models.Book> _logger;
        private readonly Mock<IBook> _bookRepository;

        public BookModelTest()
        {
            _bookRepository = new Mock<IBook>();
            _logger = new Mock<ILogger<Models.Book>>().Object;
        }

        [Fact]
        public void AddBook_ReturnsAddedBook()
        {
            var bookDto = new Domain.DTOs.Book { BookID = 2, ISBN = "324235", Author = "Test Author" };
            var addedBook = new Domain.Entities.Book { ItemID = 1, ISBN = "324235", Author = "Test Author" };
            _bookRepository.Setup(x => x.AddBook(bookDto)).Returns(addedBook);
            var bookModel = new Models.Book(_bookRepository.Object, _logger);

            var result = bookModel.AddBook(bookDto);

            Assert.NotNull(result);
            Assert.Equal(bookDto.ISBN, result.ISBN);
            Assert.Equal(bookDto.Author, result.Author);
            Assert.NotEqual(bookDto.BookID, result.ItemID);
            _bookRepository.Verify(x => x.AddBook(bookDto), Times.Once());
        }
        [Fact]
        public void UpdateBook_Should_Return_Updated_Book()
        {
            var bookID = 1;
            var bookDto = new Domain.DTOs.Book { BookID = 2, ISBN = "324235", Author = "Test Author" };
            var updatedBook = new Domain.Entities.Book { ItemID = bookID, ISBN = "324235", Author = "Test Author" };
            _bookRepository.Setup(x => x.UpdateBook(bookID, bookDto)).Returns(updatedBook);
            var bookModel = new Models.Book(_bookRepository.Object, _logger);

            var result = bookModel.UpdateBook(bookID, bookDto);

            Assert.True(result.ItemID == 1);
            Assert.Equal(updatedBook.ItemID, result.ItemID);
            Assert.Equal(updatedBook.ISBN, result.ISBN);
            Assert.Equal(updatedBook.Author, result.Author);
            _bookRepository.Verify(x => x.UpdateBook(bookID, bookDto), Times.Once());
        }
        [Fact]
        public void DeleteBook_ReturnsDeletedBook()
        {
            int bookIdToDelete = 1;
            var expectedDeletedBook = new Domain.Entities.Book { ItemID = bookIdToDelete, ISBN = "1234567890", Author = "Test Author" };
            _bookRepository.Setup(x => x.DeleteBook(bookIdToDelete)).Returns(expectedDeletedBook);
            var bookModel = new Models.Book(_bookRepository.Object, _logger);

            var deletedBook = bookModel.DeleteBook(bookIdToDelete);

            _bookRepository.Verify(x => x.DeleteBook(bookIdToDelete), Times.Once());
            Assert.Equal(expectedDeletedBook, deletedBook);
        }
        [Fact]
        public void GetAllBooks_ReturnsAllBooks()
        {
            var expectedBooks = new List<Domain.Entities.Book>
        {
            new Domain.Entities.Book { ItemID = 1, ISBN = "1234567890", Author = "Test Author 1" },
            new Domain.Entities.Book { ItemID = 2, ISBN = "0987654321", Author = "Test Author 2" },
            new Domain.Entities.Book { ItemID = 3, ISBN = "5555555555", Author = "Test Author 3" }
        };
            _bookRepository.Setup(x => x.GetAllBooks()).Returns(expectedBooks);
            var bookModel = new Models.Book(_bookRepository.Object, _logger);

            var allBooks = bookModel.GetAllBooks();

            _bookRepository.Verify(x => x.GetAllBooks(), Times.Once());
            Assert.Equal(expectedBooks, allBooks);
        }
        [Fact]
        public void GetBookById_ReturnsCorrectBook()
        {
            var bookId = 1;
            var expectedBook = new Domain.Entities.Book { ItemID = bookId, ISBN = "1234567890", Author = "Test Author" };
            _bookRepository.Setup(x => x.GetBookById(bookId)).Returns(expectedBook);
            var bookModel = new Models.Book(_bookRepository.Object, _logger);

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
            var bookModel = new Models.Book(_bookRepository.Object, _logger);


            var bookDTO = bookModel.GetBookByIdResponse(bookId);

            _bookRepository.Verify(x => x.GetBookByIdResponse(bookId), Times.Once());
            Assert.Equal(expectedBookDTO, bookDTO);
        }
        [Fact]
        public void GetAllBooksResponse_ReturnsExpectedResult()
        {
            var bookModel = new Models.Book(_bookRepository.Object, _logger);
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
            var bookModel = new Models.Book(_bookRepository.Object, _logger);
            var bookId = 1;
            var expectedCopyId = 2;
            var expectedCopy = new Domain.Entities.Copy { CopyID = expectedCopyId, ItemID = bookId };
            _bookRepository.Setup(x => x.AddCopyOfBookById(bookId)).Returns(expectedCopy);

            var addedCopy = bookModel.AddCopyOfBookById(bookId);

            Assert.NotNull(addedCopy);
            Assert.Equal(expectedCopyId, addedCopy.CopyID);
            Assert.Equal(bookId, addedCopy.ItemID);
            _bookRepository.Verify(x => x.AddCopyOfBookById(bookId), Times.Once);
        }
        [Fact]
        public void AddCopyOfBookByIdResponse_ReturnsAddedCopy()
        {
            var bookModel = new Models.Book(_bookRepository.Object, _logger);
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
