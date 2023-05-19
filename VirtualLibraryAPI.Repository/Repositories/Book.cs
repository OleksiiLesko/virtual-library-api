using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Domain;
using VirtualLibraryAPI.Domain.DTOs;
using VirtualLibraryAPI.Domain.Entities;
using static System.Reflection.Metadata.BlobBuilder;
using Type = VirtualLibraryAPI.Domain.Entities.Type;

namespace VirtualLibraryAPI.Repository.Repositories
{
    /// <summary>
    /// Book repository 
    /// </summary>
    public class Book : IBook
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
        public Domain.Entities.Book AddBook(Domain.DTOs.Book book)
        {
            var newBook = new Domain.Entities.Book()
            {
                Author = book.Author,
                ISBN = book.ISBN
            };
            var item = new Item()
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
            return newBook;
        }
        /// <summary>
        /// Delete book from the database
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public Domain.Entities.Book DeleteBook(int bookId)
        {
            var book = _context.Books.Find(bookId);
            if (book == null)
            {
                return null;
            }
            _context.Books.Remove(book);

            var item = _context.Items.FirstOrDefault(i => i.ItemID == bookId);
            if (item != null)
            {
                _context.Items.Remove(item);
            }

            _context.SaveChanges();

            _logger.LogInformation("Deleting book from database: {BookID}", book.ItemID);

            return book;
        }
        /// <summary>
        /// Return all books from the database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Domain.Entities.Book> GetAllBooks()
        {
            var books = _context.Books.ToList();
            _logger.LogInformation("Returning all books from the database");

            return books;
        }
        /// <summary>
        /// Get book by id from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Entities.Book GetBookById(int id)
        {
            _logger.LogInformation($"Getting book with copies by id: BookID {id}");
            return _context.Books.FirstOrDefault(b => b.ItemID == id);
        }
        /// <summary>
        /// Update book by id in the database
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        public Domain.Entities.Book UpdateBook(int bookId, Domain.DTOs.Book book)
        {
            var existingBook = _context.Books.Find(bookId);
            if (existingBook == null)
            {
                return null;
            }
            existingBook.Author = book.Author;
            existingBook.ISBN = book.ISBN;
            var item = _context.Items.FirstOrDefault(i => i.ItemID == bookId);
            if (item == null)
            {
                return null;
            }

            item.Name = book.Name;
            item.PublishingDate = (DateTime)book.PublishingDate;
            item.Publisher = book.Publisher;

            _context.SaveChanges();
            _logger.LogInformation("Update book by id in the database: {BookID}", existingBook.ItemID);
            return existingBook;
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
                                   .Where(x => x.Item.Type == Type.Book)
                                  .FirstOrDefault(x => x.Book.ItemID == id);

            if (result == null)
            {
                return null;
            }
            _logger.LogInformation($"Get book by id for response:BookID {id}");
            return new Domain.DTOs.Book
            {
                CountOfCopies = GetNumberOfCopiesOfBookById(id),
                Name = result.Item.Name,
                PublishingDate = result.Item.PublishingDate,
                Publisher = result.Item.Publisher,
                ISBN = result.Book.ISBN,
                Author = result.Book.Author
            };
        }
        /// <summary>
        /// Get all books for response DTO
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Domain.DTOs.Book> GetAllBooksResponse()
        {
            _logger.LogInformation(" Get all books for response DTO:");
            return _context.Items
                           .Join(_context.Books, item => item.ItemID, book => book.ItemID, (item, book) => new { Item = item, Book = book })
                            .Where(x => x.Item.Type == Type.Book)
                           .Select(x => new Domain.DTOs.Book
                           {
                               BookID = x.Item.ItemID,
                               CountOfCopies = _context.Copies.Count(c => c.ItemID == x.Item.ItemID),
                               Name = x.Item.Name,
                               PublishingDate = x.Item.PublishingDate,
                               Publisher = x.Item.Publisher,
                               ISBN = x.Book.ISBN,
                               Author = x.Book.Author
                           });
        }
        /// <summary>
        /// Get number of books copies
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public int GetNumberOfCopiesOfBookById(int bookId)
        {
            _logger.LogInformation($"Getting number of copies for book with id: {bookId}");
            return _context.Copies.Count(c => c.ItemID == bookId);
        }
        /// <summary>
        /// Add copies of Book by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="numberOfCopies"></param>
        /// <returns></returns>
        public Domain.Entities.Copy AddCopyOfBookById(int id)
        {
            var existingBook = _context.Books.Find(id);
            if (existingBook == null)
            {
                return null;
            }

            var newCopy = new Domain.Entities.Copy
            {
                ItemID = id
            };
            _context.Copies.Add(newCopy);
            _context.SaveChanges();

            _logger.LogInformation("Adding a copy of a book to the database: {CopyId}", id);
            return newCopy;
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

            if (result == null)
            {
                return null;
            }

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

