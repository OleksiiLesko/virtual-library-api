 using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Common;
using VirtualLibraryAPI.Domain;
using VirtualLibraryAPI.Domain.DTOs;
using VirtualLibraryAPI.Domain.Entities;
using static System.Reflection.Metadata.BlobBuilder;
using DepartmentType = VirtualLibraryAPI.Common.DepartmentType;
using Type = VirtualLibraryAPI.Domain.Entities.Type;

namespace VirtualLibraryAPI.Repository.Repositories
{
    /// <summary>
    /// Book repository 
    /// </summary>
    public class Book : IBookRepository
    {
        /// <summary>
        /// Application context
        /// </summary>
        private readonly ApplicationContext _context;
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<Book> _logger;
        /// <summary>
        /// Constructor with context and logger
        /// </summary>
        /// <param name="context"></param>
        public Book(ApplicationContext context, ILogger<Book> logger)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// Add book to the database
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public Domain.DTOs.Book AddBook(Domain.DTOs.Book book,DepartmentType  departmentType)
        {
            var newBook = new Domain.Entities.Book()
            {
                Author = book.Author,
                ISBN = book.ISBN
            };
            var item = new Domain.Entities.Item()
            {
                Type = Type.Book,
                Name = book.Name,
                PublishingDate = book.PublishingDate,
                Publisher = book.Publisher,
                Book = newBook
            };
            _context.Items.Add(item);
            _context.SaveChanges();
            _logger.LogInformation("Adding book to the database: {BookID}", newBook.ItemID);
            var addedBook = new Domain.DTOs.Book
            {
                BookID = newBook.ItemID,
                DepartmentType = departmentType,
                CopyID = null, 
                Name = book.Name,
                PublishingDate = book.PublishingDate,
                Publisher = book.Publisher,
                Author = book.Author,
                ISBN = book.ISBN,
                CopyInfo = null 
            };

            return addedBook;
        }
        /// <summary>
        /// Delete book from the database
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public Domain.DTOs.Book DeleteBook(int bookId)
        {
            var bookEntity = _context.Books.Find(bookId);

            var copies = _context.Copies.Where(c => c.ItemID == bookId).ToList();
            foreach (var copyEntity in copies)
            {
                _context.Copies.Remove(copyEntity);
            }

            var itemEntity = _context.Items.FirstOrDefault(i => i.ItemID == bookId);

            var deletedBookDto = new Domain.DTOs.Book
            {
                Name = itemEntity.Name,
                PublishingDate = itemEntity.PublishingDate,
                Publisher = itemEntity.Publisher,
                Author = bookEntity.Author,
                ISBN = bookEntity.ISBN,
                CopyInfo = bookEntity.CopyInfo
            };

            _context.Books.Remove(bookEntity);

            if (itemEntity != null)
            {
                _context.Items.Remove(itemEntity);
            }

            _context.SaveChanges();

            _logger.LogInformation("Deleting book from database: {BookID}", bookEntity.ItemID);

            return deletedBookDto;
        }
        /// <summary>
        /// Return all books from the database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Domain.DTOs.Book> GetAllBooks()
        {
            var bookEntities = _context.Books.ToList();
            _logger.LogInformation("Returning all books from the database");

            var bookDtos = new List<Domain.DTOs.Book>();

            foreach (var bookEntity in bookEntities)
            {
                var bookDto = new Domain.DTOs.Book();

                bookDtos.Add(bookDto);
            }

            return bookDtos;
        }
        /// <summary>
        /// Get book by id from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.DTOs.Book GetBookById(int id)
        {
            _logger.LogInformation($"Getting book with copies by id: BookID {id}");
            var bookEntity = _context.Books.FirstOrDefault(b => b.ItemID == id);
            if (bookEntity == null)
            {
                return null;
            }

            var itemEntity = _context.Items.FirstOrDefault(i => i.ItemID == id);

            if (itemEntity == null)
            {
                return null;
            }

            var bookDto = new Domain.DTOs.Book
            {
                BookID = id,
                CopyID = null,
                Name = itemEntity.Name,
                PublishingDate = itemEntity.PublishingDate,
                Publisher = itemEntity.Publisher,
                Author = bookEntity.Author,
                ISBN = bookEntity.ISBN,
                CopyInfo = bookEntity.CopyInfo
            };

            return bookDto;
        }
        /// <summary>
        /// Update book by id in the database
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        public Domain.DTOs.Book UpdateBook(int bookId, Domain.DTOs.Book book, DepartmentType departmentTypes)
        {
            var existingBook = _context.Books.Find(bookId);

            existingBook.Author = book.Author;
            existingBook.ISBN = book.ISBN;
            var item = _context.Items.FirstOrDefault(i => i.ItemID == bookId);

            item.Name = book.Name;
            item.PublishingDate = (DateTime)book.PublishingDate;
            item.Publisher = book.Publisher;

            _context.SaveChanges();
            _logger.LogInformation("Update book by id in the database: {BookID}", existingBook.ItemID);
            return book;
        }
        /// <summary>
        /// Get book by id for response
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.DTOs.Book GetBookByIdResponse(int id)
        {
            var result = _context.Items
                .Join(_context.Books, item => item.ItemID, book => book.ItemID, (item, book) => new { Item = item, Book = book })
                .Where(x => x.Item.Type == Type.Book && x.Book.ItemID == id)
                .FirstOrDefault();

            var bookDto = new Domain.DTOs.Book
            {
                BookID = id,
                CopyID = null,
                Name = result.Item.Name,
                PublishingDate = result.Item.PublishingDate,
                Publisher = result.Item.Publisher,
                ISBN = result.Book.ISBN,
                Author = result.Book.Author,
                CopyInfo = new CopyInfo
                {
                    CountOfCopies = GetNumberOfCopiesOfBookById(id),
                    CopiesAvailability = 0
                }
            };

            bookDto.CopyInfo.CopiesAvailability = _context.Copies.Count(c => c.ItemID == bookDto.BookID && c.IsAvailable);

            return bookDto;

           
        }
        /// <summary>
        /// Get all books for response DTO
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Domain.DTOs.Book> GetAllBooksResponse()
        {
            _logger.LogInformation(" Get all books for response DTO:");
            var books =  _context.Items
                           .Join(_context.Books, item => item.ItemID, book => book.ItemID, (item, book) => new { Item = item, Book = book })
                            .Where(x => x.Item.Type == Type.Book)
                           .Select(x => new Domain.DTOs.Book
                           {
                               BookID = x.Item.ItemID,
                               CopyInfo = new CopyInfo
                               {
                                   CountOfCopies = 0,
                                   CopiesAvailability = 0
                               },
                               Name = x.Item.Name,
                               PublishingDate = x.Item.PublishingDate,
                               Publisher = x.Item.Publisher,
                               ISBN = x.Book.ISBN,
                               Author = x.Book.Author
                           }).ToList();
            foreach (var book in books)
            {
                book.CopyInfo.CountOfCopies = _context.Copies.Count(c => c.ItemID == book.BookID);
                book.CopyInfo.CopiesAvailability = _context.Copies.Count(c => c.ItemID == book.BookID && c.IsAvailable);
            }

            return books;
        }
        /// <summary>
        /// Get number of books copies
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public int GetNumberOfCopiesOfBookById(int bookId)
        {
            _logger.LogInformation($"Getting number of copies for book with id: {bookId}");
            return _context.Copies.Count(c => c.ItemID == bookId );
        }
        /// <summary>
        /// Add copies of Book by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="numberOfCopies"></param>
        /// <returns></returns>
        public Domain.DTOs.Copy AddCopyOfBookById(int id, bool isAvailable)
        {
            var existingBook = _context.Books.Find(id);
            if (existingBook == null)
            {
                return null;
            }

            var newCopyEntity = new Domain.Entities.Copy
            {
                ItemID = id,
                IsAvailable = isAvailable,
            };

            _context.Copies.Add(newCopyEntity);
            _context.SaveChanges();

            var newCopyDto = new Domain.DTOs.Copy
            {
                CopyID = newCopyEntity.CopyID,
                ItemID = newCopyEntity.ItemID,
                IsAvailable = newCopyEntity.IsAvailable,
            };

            _logger.LogInformation("Adding a copy of a book to the database: {CopyId}", id);

            return newCopyDto;
        }

        /// <summary>
        /// Add copy of a book for response
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.DTOs.Book AddCopyOfBookByIdResponse(int id)
        {
            var result = _context.Copies
                                  .Join(_context.Books, copy => copy.ItemID, book => book.ItemID, (copy, book) => new { Copy = copy, Book = book })
                                  .Join(_context.Items, b => b.Copy.ItemID, item => item.ItemID, (b, item) => new { Copy = b.Copy, Book = b.Book, Item = item })
                                   .Where(x => x.Item.Type == Type.Book)
                                  .Where(x => x.Copy.ItemID == id)
                                  .OrderByDescending(x => x.Copy.CopyID)
                                  .FirstOrDefault();

            _logger.LogInformation($"Add copy of a book for response: CopyID {result.Copy.CopyID}");

            return new Domain.DTOs.Book
            {
                CopyID = result.Copy.CopyID,
                Name = result.Item.Name,
                PublishingDate = result.Item.PublishingDate,
                Publisher = result.Item.Publisher,
                ISBN = result.Book.ISBN,
                Author = result.Book.Author
            };
        }
    }
}

