using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Domain;
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
        public Domain.DTOs.Copy ReserveCopyById(int copyId, int bookingPeriod)
        {
            var copy = _context.Copies.FirstOrDefault(c => c.CopyID == copyId);

            _context.SaveChanges();

            _logger.LogInformation("Copy booked: {CopyID}", copy.CopyID);
            var copyDTO = new Domain.DTOs.Copy
            {
                CopyID = copyId,
                ExpirationDate = DateTime.Now.AddDays(bookingPeriod)
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
            _context.SaveChanges();

            _logger.LogInformation("Copy returned: {CopyID}", copy.CopyID);

            var copyDTO = new Domain.DTOs.Copy();
            return copyDTO;
        }
    }
}
