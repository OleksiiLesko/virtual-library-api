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
    public class Copy : ICopyRepository
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

        public Domain.DTOs.Copy DeleteCopy(int id)
        {
            var copy = _context.Copies.Find(id);
            _context.Copies.Remove(copy);

            var deletedCopyDto = new Domain.DTOs.Copy();
            //var item = _context.Items.FirstOrDefault(i => i.ItemID == bookId);
            //if (item != null)
            //{
            //    _context.Items.Remove(item);
            //}

            _context.SaveChanges();

            _logger.LogInformation("Deleting copy from database: {CopyID}", copy.ItemID);

            return deletedCopyDto;
        }
        public Domain.DTOs.Copy GetCopyByIdResponse(int id)
        {
            var result = _context.Items
                                 .Join(_context.Books, item => item.ItemID, book => book.ItemID, (item, book) => new { Item = item, Book = book })
                                 .FirstOrDefault(x => x.Book.ItemID == id);

            _logger.LogInformation($"Get copy by id for response:BookID {id}");
            return new Domain.DTOs.Copy
            {
                CopyID = id,
                Name = result.Item.Name,
                PublishingDate = result.Item.PublishingDate,
                Publisher = result.Item.Publisher,
                ISBN = result.Book.ISBN,
                Author = result.Book.Author
            };
        }

        public Domain.DTOs.Copy GetCopyById(int id)
        {
            _logger.LogInformation($"Get book by id from the database: BookID {id}");
            var copyEntity = _context.Copies.FirstOrDefault(b => b.CopyID == id);
            if (copyEntity == null)
            {
                return null;
            }

            var itemEntity = _context.Items.FirstOrDefault(b => b.ItemID == id);
            if (itemEntity == null)
            {
                return null;
            }

            var copyDtos = new Domain.DTOs.Copy
            {
                CopyID = copyEntity.CopyID,
                ItemID = copyEntity.ItemID,
                IsAvailable = copyEntity.IsAvailable,
                ExpirationDate = copyEntity.ExpirationDate,
                Type = (Domain.DTOs.Type)itemEntity.Type
            };
            return copyDtos;
        }

        public Domain.DTOs.Copy UpdateCopy(int copyId, Domain.DTOs.Copy copy)
        {
            var existingCopy = _context.Copies.Find(copyId);

            existingCopy.ItemID = copy.ItemID;
            existingCopy.CopyID = copy.CopyID;

            var item = _context.Items.FirstOrDefault(i => i.ItemID == existingCopy.ItemID);

            item.Name = copy.Name;
            item.PublishingDate = (DateTime)copy.PublishingDate;
            item.Publisher = copy.Publisher;

            var book = _context.Books.FirstOrDefault(b => b.ItemID == existingCopy.ItemID);


            book.ISBN = copy.ISBN;
            book.Author = copy.Author;
            var copyDTO = new Domain.DTOs.Copy();
            _context.SaveChanges();
            _logger.LogInformation("Update copy by id in the database: {BookID}", existingCopy.ItemID);
            return copyDTO;
        }
    }
}
