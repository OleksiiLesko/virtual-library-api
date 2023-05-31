using Microsoft.Extensions.Logging;
using VirtualLibraryAPI.Domain.DTOs;
using VirtualLibraryAPI.Domain.Entities;
using VirtualLibraryAPI.Repository;

namespace VirtualLibraryAPI.Models
{
    /// <summary>
    /// Model for book
    /// </summary>
    public class Book : IBook
    {
        /// <summary>
        /// Using book repository
        /// </summary>
        private readonly IBook _bookRepository;
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<Book> _logger;

        /// <summary>
        /// Constructor with book Repository and logger
        /// </summary>
        /// <param name="bookRepository"></param>
        /// <param name="logger"></param>
        public Book(IBook bookRepository, ILogger<Book> logger)
        {
            _bookRepository = bookRepository;
            _logger = logger;
        }
        /// <summary>
        /// Adding book from Book model
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public Domain.Entities.Book AddBook(Domain.DTOs.Book book)
        {
            _logger.LogInformation($"Adding book from Book model {book}");
            return _bookRepository.AddBook(book);
        }
        /// <summary>
        /// Updating book from Book model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="book"></param>
        /// <returns></returns>

        public Domain.Entities.Book UpdateBook(int id, Domain.DTOs.Book book)
        {
            _logger.LogInformation($"Updating book from Book model: BookID {id}");
            return _bookRepository.UpdateBook(id, book);
        }
        /// <summary>
        /// Deleting book from Book model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public Domain.Entities.Book DeleteBook(int id)
        {
            _logger.LogInformation($"Deleting book from Book model: BookID {id}");
            return _bookRepository.DeleteBook(id);
        }
        /// <summary>
        /// Getting all books from Book model
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Domain.Entities.Book> GetAllBooks()
        {
            _logger.LogInformation($"Getting all books from Book model ");
            var books = _bookRepository.GetAllBooks();
            if (books.Any())
            {
                return books;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Getting book by id from Book model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public Domain.Entities.Book GetBookById(int id)
        {
            _logger.LogInformation($"Getting book by id from Book model: BookID {id} ") ; 
            return _bookRepository.GetBookById(id);
        }
        /// <summary>
        /// Get book by id for response DTO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.DTOs.Book GetBookByIdResponse(int id)
        {
            _logger.LogInformation($"Get book by id for response DTO from Book model: BookID {id}");
            return _bookRepository.GetBookByIdResponse(id);
        }
        /// <summary>
        /// Get all books for response DTO
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Domain.DTOs.Book> GetAllBooksResponse()
        {
            _logger.LogInformation("Get all books for response DTO from Book model  ");
            return _bookRepository.GetAllBooksResponse();
        }
        /// <summary>
        /// Add copy of a book by id  from Book model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="numberOfCopies"></param>
        /// <returns></returns>
        public Domain.Entities.Copy AddCopyOfBookById(int id, bool isAvailable)
        {
            _logger.LogInformation($"Add copy of a book by id  from Book model: CopyID {id}  ");
            return _bookRepository.AddCopyOfBookById(id, isAvailable);
        }
        /// <summary>
        /// Add copy of a book by id for response
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.DTOs.Book AddCopyOfBookByIdResponse(int id)
        {
            _logger.LogInformation($"Add copy of a book by id for response  from Book model: CopyID {id}  ");
            return _bookRepository.AddCopyOfBookByIdResponse(id);
        }
    }
}