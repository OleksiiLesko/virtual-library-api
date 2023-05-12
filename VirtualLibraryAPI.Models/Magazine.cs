using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Repository;

namespace VirtualLibraryAPI.Models
{
    /// <summary>
    /// Model for magazine
    /// </summary>
    public class Magazine : IMagazine
    {
        /// <summary>
        /// Using magazine repository
        /// </summary>
        private readonly IMagazine _magazineRepository;
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<Magazine> _logger;
        /// <summary>
        /// Constructor with magazine Repository and logger
        /// </summary>
        /// <param name="bookRepository"></param>
        /// <param name="logger"></param>
        public Magazine(IMagazine magazineRepository, ILogger<Magazine> logger)
        {
            _magazineRepository = magazineRepository;
            _logger = logger;
        }
        /// <summary>
        /// Adding copy of magazine from Magazine model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.Entities.Copy AddCopyOfMagazineById(int id)
        {
            _logger.LogInformation($"Add copy of a magazine by id  from magazine model: CopyID {id}  ");
            return _magazineRepository.AddCopyOfMagazineById(id);
        }
        /// <summary>
        /// Adding copy of magazine for response
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.DTOs.Magazine AddCopyOfMagazineByIdResponse(int id)
        {
            _logger.LogInformation($"Add copy of a magazine by id for DTO : CopyID {id}  ");
            return _magazineRepository.AddCopyOfMagazineByIdResponse(id);
        }
        /// <summary>
        /// Adding  magazine from Magazine model
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.Entities.Magazine AddMagazine(Domain.DTOs.Magazine magazine)
        {
            _logger.LogInformation($"Adding magazine from magazine model {magazine}");
            return _magazineRepository.AddMagazine(magazine);
        }
        /// <summary>
        /// Deleting magazine from Magazine model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.Entities.Magazine DeleteMagazine(int id)
        {
            _logger.LogInformation($"Deleting magazine from magazine model: MagazineID {id}");
            return _magazineRepository.DeleteMagazine(id);
        }
        /// <summary>
        /// Get all magazines from Magazine model
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<Domain.Entities.Magazine> GetAllMagazines()
        {
            _logger.LogInformation($"Getting all magazines from magazine model ");
            return _magazineRepository.GetAllMagazines();
        }
        /// <summary>
        /// Get all magazines for response
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<Domain.DTOs.Magazine> GetAllMagazinesResponse()
        {
            _logger.LogInformation("Get all magazines for response DTO from magazine model  ");
            return _magazineRepository.GetAllMagazinesResponse();
        }
        /// <summary>
        /// Get magazine by id from magazine model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.Entities.Magazine GetMagazineById(int id)
        {
            _logger.LogInformation($"Getting magazine by id from magazine model: MagazineID {id} ");
            return _magazineRepository.GetMagazineById(id);
        }
        /// <summary>
        /// Get magazine by id for response
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.DTOs.Magazine GetMagazineByIdResponse(int id)
        {
            _logger.LogInformation($"Get magazine by id for response DTO from magazine model: MagazineID {id}");
            return _magazineRepository.GetMagazineByIdResponse(id);
        }
        /// <summary>
        /// Update magazine from book model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.Entities.Magazine UpdateMagazine(int id, Domain.DTOs.Magazine magazine)
        {
            _logger.LogInformation($"Updating magazine from magazine model: MagazineID {id}");
            return _magazineRepository.UpdateMagazine(id, magazine);
        }
    }
}
