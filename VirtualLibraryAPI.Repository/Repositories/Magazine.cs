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
using VirtualLibraryAPI.Common;
using DepartmentType = VirtualLibraryAPI.Common.DepartmentType;

namespace VirtualLibraryAPI.Repository.Repositories
{
    /// <summary>
    /// Magazine repository 
    /// </summary>
    public class Magazine : IMagazineRepository
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
        public Domain.DTOs.Magazine AddMagazine(Domain.DTOs.Magazine magazine,DepartmentType departmentType)
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
                DepartmentTypes = (DepartmentTypes)departmentType,
                Magazine = newMagazine
            };
            _context.Items.Add(item);
            _context.SaveChanges();
            _logger.LogInformation("Adding magazine to the database: {MagazineID}", newMagazine.ItemID);

            var addedMagazine = new Domain.DTOs.Magazine
            {
                MagazineID = newMagazine.ItemID,
                CopyID = null,
                DepartmentType = departmentType,
                Name = magazine.Name,
                PublishingDate = magazine.PublishingDate,
                Publisher = magazine.Publisher,
                IssueNumber = magazine.IssueNumber,
                CopyInfo = null
            };

            return addedMagazine;
        }
        /// <summary>
        /// Delete magazine from the database
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public Domain.DTOs.Magazine DeleteMagazine(int magazineId)
        {
            var magazineEntity = _context.Magazines.Find(magazineId);
            _context.Magazines.Remove(magazineEntity);

            var copies = _context.Copies.Where(c => c.ItemID == magazineId).ToList();
            foreach (var copyEntity in copies)
            {
                _context.Copies.Remove(copyEntity);
            }

            var itemEntity = _context.Items.FirstOrDefault(i => i.ItemID == magazineId);

            var deletedMagazineDto = new Domain.DTOs.Magazine
            {
                DepartmentType = (DepartmentType)itemEntity.DepartmentTypes,
                Name = itemEntity.Name,
                PublishingDate = itemEntity.PublishingDate,
                Publisher = itemEntity.Publisher,
                IssueNumber = magazineEntity.IssueNumber,
                CopyInfo = magazineEntity.CopyInfo
            };

            _context.Magazines.Remove(magazineEntity);

            if (itemEntity != null)
            {
                _context.Items.Remove(itemEntity);
            }

            _context.SaveChanges();
            _logger.LogInformation("Deleting magazine from database: {MagazineID}", magazineEntity.ItemID);


            return deletedMagazineDto;
        }
        /// <summary>
        /// Return all magazines from the database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Domain.DTOs.Magazine> GetAllMagazines()
        {
            var magazineEntities = _context.Magazines.ToList();
            _logger.LogInformation("Returning all magazines from the database");

            var magazineDtos = new List<Domain.DTOs.Magazine>();

            foreach (var magazineEntity in magazineEntities)
            {
                var magazineDto = new Domain.DTOs.Magazine();

                magazineDtos.Add(magazineDto);
            }

            return magazineDtos;
        }
        /// <summary>
        /// Get magazine by id from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.DTOs.Magazine GetMagazineById(int id)
        {
            _logger.LogInformation($"Getting magazine with copies by id: MagazineID {id}");
            var magazineEntity = _context.Magazines.FirstOrDefault(b => b.ItemID == id);
            if (magazineEntity == null)
            {
                return null;
            }

            var itemEntity = _context.Items.FirstOrDefault(i => i.ItemID == magazineEntity.ItemID);

            if ( itemEntity == null)
            {
                return null;
            }

            var magazineDto = new Domain.DTOs.Magazine
            {
                MagazineID = magazineEntity.ItemID,
                CopyID = null,
                DepartmentType = (DepartmentType)itemEntity.DepartmentTypes,
                Name = itemEntity.Name,
                PublishingDate = itemEntity.PublishingDate,
                Publisher = itemEntity.Publisher,
                IssueNumber = magazineEntity.IssueNumber,
                CopyInfo = magazineEntity.CopyInfo
            };

            return magazineDto;
        }
        /// <summary>
        /// Update magazine by id in the database
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        public Domain.DTOs.Magazine UpdateMagazine(int bookId, Domain.DTOs.Magazine magazine, DepartmentType departmentTypes)
        {
            var existingMagazine = _context.Magazines.Find(bookId);
            existingMagazine.IssueNumber = magazine.IssueNumber;
            var item = _context.Items.FirstOrDefault(i => i.ItemID == bookId);

            item.DepartmentTypes = (DepartmentTypes)magazine.DepartmentType;
            item.Name = magazine.Name;
            item.PublishingDate = (DateTime)magazine.PublishingDate;
            item.Publisher = magazine.Publisher;

            _context.SaveChanges();
            _logger.LogInformation("Update magazine by id in the database: {MagazineID}", existingMagazine.ItemID);
            return magazine;
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

            _logger.LogInformation($"Get magazine by id for response:MagazineID {id}");

            var copies = _context.Copies.Count(c => c.ItemID == result.Item.Magazine.ItemID && c.IsAvailable);

            var magazineDTO = new Domain.DTOs.Magazine
            {
                CopyID = null,
                DepartmentType = (DepartmentType)result.Item.DepartmentTypes,
                CopyInfo = new CopyInfo
                {
                    CountOfCopies = GetNumberOfCopiesOfMagazineById(id),
                    CopiesAvailability = copies
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
                               DepartmentType = (DepartmentType)x.Item.DepartmentTypes,
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
        public Domain.DTOs.Copy AddCopyOfMagazineById(int id, bool isAvailable)
        {
            var existingMagazine = _context.Magazines.Find(id);
            if (existingMagazine == null)
            {
                return null;
            }

            var newCopyEntity = new Domain.Entities.Copy
            {
                ItemID = id,
                IsAvailable = isAvailable
            };

            _context.Copies.Add(newCopyEntity);
            _context.SaveChanges();

            var newCopyDto = new Domain.DTOs.Copy
            {
                CopyID = newCopyEntity.CopyID,
                ItemID = newCopyEntity.ItemID,
                IsAvailable = newCopyEntity.IsAvailable
            };
            _logger.LogInformation("Adding a copy of a magazine to the database: {CopyId}", id);
            return newCopyDto;
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

            _logger.LogInformation($"Add copy of a magazine for response: CopyID {result.Copy.CopyID}");

            return new Domain.DTOs.Magazine
            {
                CopyID = result.Copy.CopyID,
                DepartmentType = (DepartmentType)result.Item.DepartmentTypes,
                Name = result.Item.Name,
                PublishingDate = result.Item.PublishingDate,
                Publisher = result.Item.Publisher,
                IssueNumber = result.Magazine.IssueNumber
            };
        }
    }
}
