using VirtualLibraryAPI.Common;
using VirtualLibraryAPI.Domain.DTOs;
using VirtualLibraryAPI.Domain.Entities;
using DepartmentType = VirtualLibraryAPI.Common.DepartmentType;

namespace VirtualLibraryAPI.Repository
{
    /// <summary>
    /// Book repository interface 
    /// </summary>
    public interface IBookRepository
    {
        /// <summary>
        /// Method for get all books
        /// </summary>
        /// <returns></returns>
        IEnumerable<Domain.DTOs.Book> GetAllBooks();
        /// <summary>
        /// Method for get book by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.DTOs.Book GetBookById(int id);
        /// <summary>
        /// Method for add book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        Domain.DTOs.Book AddBook(Domain.DTOs.Book book, DepartmentType departmentTypes);
        /// <summary>
        /// Method for add copy of a book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        Domain.DTOs.Copy AddCopyOfBookById(int id, bool isAvailable);
        /// <summary>
        /// Method for update book
        /// </summary>
        /// <param name="id"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        Domain.DTOs.Book UpdateBook(int id, Domain.DTOs.Book book, DepartmentType departmentTypes);
        /// <summary>
        /// Method for delete book
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.DTOs.Book DeleteBook(int id);
        /// <summary>
        /// Get  book by id for response DTO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.DTOs.Book GetBookByIdResponse(int id);
        /// <summary>
        /// Get all books for response DTO
        /// </summary>
        /// <returns></returns>
        IEnumerable<Domain.DTOs.Book> GetAllBooksResponse();
        /// <summary>
        /// Add copy to book for response
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.DTOs.Book AddCopyOfBookByIdResponse(int id);

    }
}

