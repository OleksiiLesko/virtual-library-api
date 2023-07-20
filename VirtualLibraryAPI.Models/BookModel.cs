using Microsoft.Extensions.Logging;
using DepartmentType = VirtualLibraryAPI.Common.DepartmentType;
using VirtualLibraryAPI.Domain.DTOs;
using VirtualLibraryAPI.Domain.Entities;
using VirtualLibraryAPI.Repository;

namespace VirtualLibraryAPI.Models
{
    /// <summary>
    /// Model for book
    /// </summary>
    public class BookModel : IBookModel
    {
        /// <summary>
        /// Using book repository
        /// </summary>
        private readonly IBookRepository _repository;
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<BookModel> _logger;

        /// <summary>
        /// Constructor with book Repository and logger
        /// </summary>
        /// <param name="book"></param>
        /// <param name="logger"></param>
        public BookModel( ILogger<BookModel> logger, IBookRepository repository )
        {
            _logger = logger;
            _repository = repository;
        }
        /// <summary>
        /// Adding book from Book model
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public Domain.DTOs.Book AddBook(Domain.DTOs.Book book, DepartmentType departmentType)
        {
            _logger.LogInformation($"Adding book from Book model {book}");
            var result = _repository.AddBook(book, departmentType);
            if (result == null)
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// Updating book from Book model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="book"></param>
        /// <returns></returns>

        public Domain.DTOs.Book UpdateBook(int id, Domain.DTOs.Book book, DepartmentType departmentTypes)
        {
            _logger.LogInformation($"Updating book from Book model: BookID {id}");
            var result = _repository.UpdateBook(id, book, departmentTypes);
            if (result == null)
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// Deleting book from Book model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public Domain.DTOs.Book DeleteBook(int id)
        {
            _logger.LogInformation($"Deleting book from Book model: BookID {id}");
            var result = _repository.DeleteBook(id);
            if (result == null)
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// Getting all books from Book model
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Domain.DTOs.Book> GetAllBooks()
        {
            _logger.LogInformation($"Getting all books from Book model ");
            var books = _repository.GetAllBooks();
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

        public Domain.DTOs.Book GetBookById(int id)
        {
            _logger.LogInformation($"Getting book by id from Book model: BookID {id} ") ; 
            var result = _repository.GetBookById(id);
            if (result == null)
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// Get book by id for response DTO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.DTOs.Book GetBookByIdResponse(int id)
        {
            _logger.LogInformation($"Get book by id for response DTO from Book model: BookID {id}");
            var result = _repository.GetBookByIdResponse(id);
            if (result == null)
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// Get all books for response DTO
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Domain.DTOs.Book> GetAllBooksResponse()
        {
            _logger.LogInformation("Get all books for response DTO from Book model  ");
            var result = _repository.GetAllBooksResponse();
            if (result == null)
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// Add copy of a book by id  from Book model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="numberOfCopies"></param>
        /// <returns></returns>
        public Domain.DTOs.Copy AddCopyOfBookById(int id, bool isAvailable)
        {
            _logger.LogInformation($"Add copy of a book by id  from Book model: CopyID {id}  ");
            var result = _repository.AddCopyOfBookById(id, isAvailable);
            if (result == null)
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// Add copy of a book by id for response
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.DTOs.Book AddCopyOfBookByIdResponse(int id)
        {
            _logger.LogInformation($"Add copy of a book by id for response  from Book model: CopyID {id}  ");
            var result = _repository.AddCopyOfBookByIdResponse(id);
            if (result == null)
            {
                return result;
            }
            return result;
        }
    }
}