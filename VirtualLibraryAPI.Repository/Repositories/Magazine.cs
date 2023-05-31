using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Domain.Entities;
using VirtualLibraryAPI.Domain;
using Type = VirtualLibraryAPI.Domain.Entities.Type;
using VirtualLibraryAPI.Domain.DTOs;
using Microsoft.EntityFrameworkCore;

namespace VirtualLibraryAPI.Repository.Repositories
{
    /// <summary>
    /// Magazine repository 
    /// </summary>
    public class Magazine : IMagazine
    {
        /// <summary>
        /// Application context
        /// </summary>
        private readonly ApplicationContext _context;
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<Magazine> _logger;
        /// <summary>
        /// Constructor with context and logger
        /// </summary>
        /// <param name="context"></param>
        public Magazine(ApplicationContext context, ILogger<Magazine> logger)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// Add magazine to the database
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public Domain.Entities.Magazine AddMagazine(Domain.DTOs.Magazine magazine)
        {
            var newMagazine = new Domain.Entities.Magazine()
            {
                IssueNumber = magazine.IssueNumber
            };
            var item = new Domain.Entities.Item
            {
                Type = Type.Magazine,
                Name = magazine.Name,
                PublishingDate = (DateTime)magazine.PublishingDate,
                Publisher = magazine.Publisher,
                Magazine = newMagazine
            };
            _context.Items.Add(item);
            _context.SaveChanges();
            _logger.LogInformation("Adding magazine to the database: {MagazineID}", newMagazine.ItemID);
            return newMagazine;
        }
        /// <summary>
        /// Delete magazine from the database
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public Domain.Entities.Magazine DeleteMagazine(int magazineId)
        {
            var magazine = _context.Magazines.Find(magazineId);
            if (magazine == null)
            {
                return null;
            }
            _context.Magazines.Remove(magazine);

            var item = _context.Items.FirstOrDefault(i => i.ItemID == magazineId);
            if (item != null)
            {
                _context.Items.Remove(item);
            }

            _context.SaveChanges();

            _logger.LogInformation("Deleting magazine from database: {MagazineID}", magazine.ItemID);

            return magazine;
        }
        /// <summary>
        /// Return all magazines from the database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Domain.Entities.Magazine> GetAllMagazines()
        {
            var magazines = _context.Magazines.ToList();
            _logger.LogInformation("Returning all magazines from the database");

            return magazines;
        }
        /// <summary>
        /// Get magazine by id from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Entities.Magazine GetMagazineById(int id)
        {
            _logger.LogInformation($"Getting magazine with copies by id: MagazineID {id}");
            return _context.Magazines.FirstOrDefault(b => b.ItemID == id);
        }
        /// <summary>
        /// Update magazine by id in the database
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        public Domain.Entities.Magazine UpdateMagazine(int bookId, Domain.DTOs.Magazine magazine)
        {
            var existingMagazine = _context.Magazines.Find(bookId);
            if (existingMagazine == null)
            {
                return null;
            }
            existingMagazine.IssueNumber = magazine.IssueNumber;
            var item = _context.Items.FirstOrDefault(i => i.ItemID == bookId);
            if (item == null)
            {
                return null;
            }

            item.Name = magazine.Name;
            item.PublishingDate = (DateTime)magazine.PublishingDate;
            item.Publisher = magazine.Publisher;

            _context.SaveChanges();
            _logger.LogInformation("Update magazine by id in the database: {MagazineID}", existingMagazine.ItemID);
            return existingMagazine;
        }
        /// <summary>
        /// Get magazine by id for response
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.DTOs.Magazine GetMagazineByIdResponse(int id)
        {
            var result = _context.Items
                                  .Join(_context.Magazines, item => item.ItemID, magazine => magazine.ItemID, (item, magazine) => new { Item = item, Magazine = magazine })
                                    .Where(x => x.Item.Type == Type.Magazine)
                                  .FirstOrDefault(x => x.Magazine.ItemID == id);

            if (result == null)
            {
                return null;
            }
            _logger.LogInformation($"Get magazine by id for response:MagazineID {id}");

            var copies = _context.Copies.Where(c => c.ItemID == result.Item.ItemID && c.IsAvailable).ToList();

            var magazineDTO = new Domain.DTOs.Magazine
            {
                CopyInfo = new CopyInfo
                {
                    CountOfCopies = GetNumberOfCopiesOfMagazineById(id),
                    CopiesAvailability = copies.Count
        },
                Name = result.Item.Name,
                PublishingDate = result.Item.PublishingDate,
                Publisher = result.Item.Publisher,
                IssueNumber = result.Magazine.IssueNumber
            };

            return magazineDTO;
        }
        /// <summary>
        /// Get all magazines for response DTO
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Domain.DTOs.Magazine> GetAllMagazinesResponse()
        {
            _logger.LogInformation(" Get all magazines for response DTO:");
            var magazines = _context.Items
                           .Join(_context.Magazines, item => item.ItemID, magazine => magazine.ItemID, (item, magazine) => new { Item = item, Magazine = magazine })
                             .Where(x => x.Item.Type == Type.Magazine)
                           .Select(x => new Domain.DTOs.Magazine
                           {
                               MagazineID = x.Item.ItemID,
                               CopyInfo = new CopyInfo
                               {
                                   CountOfCopies = 0,
                                   CopiesAvailability = 0
                               },
                               Name = x.Item.Name,
                               PublishingDate = x.Item.PublishingDate,
                               Publisher = x.Item.Publisher,
                               IssueNumber = x.Magazine.IssueNumber
                           }).ToList();
            foreach (var magazine in magazines)
            {
                magazine.CopyInfo.CountOfCopies = _context.Copies.Count(c => c.ItemID == magazine.MagazineID);
                magazine.CopyInfo.CopiesAvailability = _context.Copies.Count(c => c.ItemID == magazine.MagazineID && c.IsAvailable);
            }

            return magazines;
        }
        /// <summary>
        /// Get number of magazines copies
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public int GetNumberOfCopiesOfMagazineById(int magazineId)
        {
            _logger.LogInformation($"Getting number of copies for magazine with id: {magazineId}");
            return _context.Copies.Count(c => c.ItemID == magazineId);
        }
        /// <summary>
        /// Add copies of magazine by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="numberOfCopies"></param>
        /// <returns></returns>
        public Domain.Entities.Copy AddCopyOfMagazineById(int id, bool isAvailable)
        {
            var existingMagazine = _context.Magazines.Find(id);
            if (existingMagazine == null)
            {
                return null;
            }

            var newCopy = new Domain.Entities.Copy
            {
                ItemID = id,
                IsAvailable = isAvailable
            };
            _context.Copies.Add(newCopy);
            _context.SaveChanges();

            _logger.LogInformation("Adding a copy of a magazine to the database: {CopyId}", id);
            return newCopy;
        }
        /// <summary>
        /// Add copy of a magazine for response
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.DTOs.Magazine AddCopyOfMagazineByIdResponse(int id)
        {
            var result = _context.Copies
                                  .Join(_context.Magazines, copy => copy.ItemID, magazine => magazine.ItemID, (copy, magazine) => new { Copy = copy, Magazine = magazine })
                                  .Join(_context.Items, b => b.Copy.ItemID, item => item.ItemID, (b, item) => new { Copy = b.Copy, Magazine = b.Magazine, Item = item })
                                    .Where(x => x.Item.Type == Type.Magazine)
                                  .Where(x => x.Copy.ItemID == id)
                                  .OrderByDescending(x => x.Copy.CopyID)
                                  .FirstOrDefault();

            if (result == null)
            {
                return null;
            }

            _logger.LogInformation($"Add copy of a magazine for response: CopyID {result.Copy.CopyID}");

            return new Domain.DTOs.Magazine
            {
                CopyID = result.Copy.CopyID,
                Name = result.Item.Name,
                PublishingDate = result.Item.PublishingDate,
                Publisher = result.Item.Publisher,
                IssueNumber = result.Magazine.IssueNumber
            };
        }
    }
}
