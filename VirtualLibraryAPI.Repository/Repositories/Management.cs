using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Common;
using VirtualLibraryAPI.Domain;
using VirtualLibraryAPI.Domain.DTOs;
using VirtualLibraryAPI.Domain.Entities;

namespace VirtualLibraryAPI.Repository.Repositories
{
    /// <summary>
    /// Management repository 
    /// </summary>
    public class Management : IManagementRepository
    {
        /// <summary>
        /// Application context
        /// </summary>
        private readonly ApplicationContext _context;
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<Management> _logger;
        /// <summary>
        /// Constructor with context and logger
        /// </summary>
        /// <param name="context"></param>
        public Management(ApplicationContext context, ILogger<Management> logger)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// Reserve copy by Id
        /// </summary>
        /// <param name="copyId"></param>
        /// <param name="bookingPeriod"></param>
        /// <returns></returns>
        public Domain.DTOs.Copy ReserveCopyById(int userId,int copyId, int bookingPeriod)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserID == userId);
            var copy = _context.Copies.FirstOrDefault(c => c.CopyID == copyId);

            copy.IsAvailable = false;
            copy.ExpirationDate = DateTime.Now.AddDays(bookingPeriod);
            copy.ClientID = userId;
            _context.SaveChanges();

            _logger.LogInformation("Copy booked: {CopyID}", copy.CopyID);
            var copyDTO = new Domain.DTOs.Copy
            {
                ClientID = user.UserID,
                CopyID = copy.CopyID,
                IsAvailable = copy.IsAvailable,
                ExpirationDate = copy.ExpirationDate
            };
            return copyDTO;
        }

        /// <summary>
        /// Return copy by Id
        /// </summary>
        /// <param name="copyId"></param>
        /// <param name="bookingPeriod"></param>
        /// <returns></returns>
        public Domain.DTOs.Copy ReturnCopyById(int copyId)
        {
            var copy = _context.Copies.FirstOrDefault(c => c.CopyID == copyId);
            copy.ClientID = null;
            copy.IsAvailable = true;
            copy.ExpirationDate = null;
            _context.SaveChanges();

            _logger.LogInformation("Copy returned: {CopyID}", copy.CopyID);

            var copyDTO = new Domain.DTOs.Copy();
            return copyDTO;
        }
        /// <summary>
        /// Return items whose expiration date has passed
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Domain.DTOs.Item> GetAllExpiredItems()
        {
            var expiredCopies = _context.Copies.Where(c => c.ExpirationDate < DateTime.Now).ToList();
            var expiredItems = new List<Domain.DTOs.Item>();

            foreach (var copy in expiredCopies)
            {
                var item = _context.Items.FirstOrDefault(i => i.ItemID == copy.ItemID);

                var expiredItem = new Domain.DTOs.Item();

                expiredItems.Add(expiredItem);
            }

            _logger.LogInformation("Returning all expired items from the database");

            return expiredItems;
        }

        /// <summary>
        /// Get all items with expired booking for response DTO
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Domain.DTOs.Copy> GetAllExpiredItemsResponse()
        {
            _logger.LogInformation(" Returning all expired items for response DTO:");
            var items = _context.Items
                           .Join(_context.Copies, item => item.ItemID, copy => copy.ItemID, (item, copy) => new { Item = item, Copy = copy })
                           .Where(copy => copy.Copy.ExpirationDate < DateTime.Now)
                           .Select(x => new Domain.DTOs.Copy
                           {
                               ItemID = x.Item.ItemID,
                               CopyID = x.Copy.CopyID,
                               Name = x.Item.Name,
                               Type = ((Common.Type)x.Item.Type),
                               ExpirationDate = x.Copy.ExpirationDate,
                               Publisher = null,
                               Author = null,
                               ISBN = null
                              
                           }).ToList();

            return items;
        }
    }
}
