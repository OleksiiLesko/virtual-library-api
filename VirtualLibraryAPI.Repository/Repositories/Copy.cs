using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Domain;

namespace VirtualLibraryAPI.Repository.Repositories
{
    /// <summary>
    /// Copy repository 
    /// </summary>
    public class Copy : ICopy
    {
        /// <summary>
        /// Application context
        /// </summary>
        private readonly ApplicationContext _context;
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<Copy> _logger;
        /// <summary>
        /// Constructor with context and logger
        /// </summary>
        /// <param name="context"></param>
        public Copy(ApplicationContext context, ILogger<Copy> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Domain.Entities.Copy DeleteCopy(int id)
        {
            var copy = _context.Copies.Find(id);
            if (copy == null)
            {
                return null;
            }
            _context.Copies.Remove(copy);

            //var item = _context.Items.FirstOrDefault(i => i.ItemID == bookId);
            //if (item != null)
            //{
            //    _context.Items.Remove(item);
            //}

            _context.SaveChanges();

            _logger.LogInformation("Deleting copy from database: {CopyID}", copy.ItemID);

            return copy;
        }
        public Domain.DTOs.Copy GetCopyByIdResponse(int id)
        {
            var result = _context.Items
                                 .Join(_context.Books, item => item.ItemID, book => book.ItemID, (item, book) => new { Item = item, Book = book })
                                 .FirstOrDefault(x => x.Book.ItemID == id);

            if (result == null)
            {
                return null;
            }
            _logger.LogInformation($"Get copy by id for response:BookID {id}");
            return new Domain.DTOs.Copy
            {
                ItemID = result.Item.ItemID,
                CopyID = id,
                Name = result.Item.Name,
                PublishingDate = result.Item.PublishingDate,
                Publisher = result.Item.Publisher,
                ISBN = result.Book.ISBN,
                Author = result.Book.Author
            };
        }

        public Domain.Entities.Copy GetCopyById(int id)
        {
            _logger.LogInformation($"Get book by id from the database: BookID {id}");
            return _context.Copies.FirstOrDefault(b => b.ItemID == id);
        }

        public Domain.Entities.Copy UpdateCopy(int copyId, Domain.DTOs.Copy copy)
        {
            var existingCopy = _context.Copies.Find(copyId);
            if (existingCopy == null)
            {
                return null;
            }

            existingCopy.ItemID = copy.ItemID;
            existingCopy.CopyID = copy.CopyID;

            var item = _context.Items.FirstOrDefault(i => i.ItemID == existingCopy.ItemID);
            if (item == null)
            {
                return null;
            }

            item.Name = copy.Name;
            item.PublishingDate = (DateTime)copy.PublishingDate;
            item.Publisher = copy.Publisher;

            var book = _context.Books.FirstOrDefault(b => b.ItemID == existingCopy.ItemID);
            if (book == null)
            {
                return null;
            }

            book.ISBN = copy.ISBN;
            book.Author = copy.Author;

            _context.SaveChanges();
            _logger.LogInformation("Update copy by id in the database: {BookID}", existingCopy.ItemID);
            return existingCopy;
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
                           .Select(x => new Domain.DTOs.Book
                           {
                               BookID = x.Item.ItemID,
                               Name = x.Item.Name,
                               PublishingDate = x.Item.PublishingDate,
                               Publisher = x.Item.Publisher,
                               ISBN = x.Book.ISBN,
                               Author = x.Book.Author
                           });
        }
    }
}
