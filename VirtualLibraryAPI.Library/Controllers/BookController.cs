using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using VirtualLibraryAPI.Common;
using VirtualLibraryAPI.Domain.DTOs;
using VirtualLibraryAPI.Models;

namespace VirtualLibraryAPI.Library.Controllers
{
    /// <summary>
    ///  Book controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<BookController> _logger;
        /// <summary>
        /// Book model
        /// </summary>
        private readonly IBookModel _bookModel;
        /// <summary>
        /// Book model
        /// </summary>
        private readonly IDepartmentModel _departmentModel;

        /// <summary>
        /// Constructor with logger,context and model
        /// </summary>
        /// <param name="logger"></param>
        public BookController(ILogger<BookController> logger, IBookModel bookModel,IDepartmentModel departmentModel)
        {
            _logger = logger;
            _bookModel = bookModel;
            _departmentModel = departmentModel;
        }
        /// <summary>
        /// Get all books
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                _logger.LogInformation("Get all books ");
                var books = _bookModel.GetAllBooks();
                if (books == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Books received ");
                return Ok(_bookModel.GetAllBooksResponse());
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }
        /// <summary>
        /// Add Book
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddBook([FromBody] Domain.DTOs.Book request)
        {
            try
            {
                var department = _departmentModel.GetDepartmentById(request.DepartmentID);
                if (department == null)
                {
                    return BadRequest("Invalid DepartmentID. Department with the specified ID does not exist.");
                }
                var addedBook = _bookModel.AddBook(request);
                if (addedBook == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Adding book:{BookID}", addedBook.BookID);

                _logger.LogInformation("Book added");
                return Ok(new Domain.DTOs.Book
                {
                    BookID = addedBook.BookID,
                    DepartmentID = addedBook.DepartmentID,
                    Name = request.Name,
                    PublishingDate = request.PublishingDate,
                    Publisher = request.Publisher,
                    ISBN = addedBook.ISBN,
                    Author = addedBook.Author
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }

        /// <summary>
        /// Add copies of a book by id
        /// </summary>
        /// <returns></returns>
        [HttpPost("{id}/Copy")]
        public IActionResult AddCopyOfBookById(int id)
        {
            try
            {
                var addedBook = _bookModel.AddCopyOfBookById(id, isAvailable: true);
                if (addedBook == null)
                {
                    return NotFound();
                }

                _logger.LogInformation("Adding copy:{CopyID}", id);

                _logger.LogInformation("Copy added");
                return Ok(_bookModel.AddCopyOfBookByIdResponse(id));
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }
        /// <summary>
        ///  Get book by Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            try
            {
                var book = _bookModel.GetBookById(id);

                if (book == null)
                {
                    return NotFound();
                }

                _logger.LogInformation("Getting book by ID:{BookID}", book.BookID);
                _logger.LogInformation("Book received ");
                return Ok(_bookModel.GetBookByIdResponse(id));
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }
        /// <summary>
        /// Update book by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult UpdateBook(int id, [FromBody] Domain.DTOs.Book request)
        {
            try
            {
                var department = _departmentModel.GetDepartmentById(request.DepartmentID);
                if (department == null)
                {
                    return BadRequest("Invalid DepartmentID. Department with the specified ID does not exist.");
                }
                var updatedBook = _bookModel.UpdateBook(id, request);
                if (updatedBook == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Updating book by ID:{BookID}", updatedBook.BookID);
                _logger.LogInformation("Book updated ");

                return Ok(new Domain.DTOs.Book
                {
                    BookID = updatedBook.BookID,
                    DepartmentID = updatedBook.DepartmentID,
                    Name = request.Name,
                    Author = updatedBook.Author,
                    ISBN = updatedBook.ISBN,
                    Publisher = request.Publisher,
                    PublishingDate = request.PublishingDate
                });
            }

            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }
        /// <summary>
        /// Delete book
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteBook(int id)
        {
            try
            {
                var book = _bookModel.GetBookById(id);
                if (book == null)
                {
                    return NotFound();
                }

                _bookModel.DeleteBook(id);
                _logger.LogInformation("Deleting book by ID:{BookID}", book.BookID);
                _logger.LogInformation("Book deleted ");

                return NoContent();
            }

            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }

    }
}

