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
    public class MagazineModel : IMagazineModel
    {
        /// <summary>
        /// Using magazine repository
        /// </summary>
        private readonly IMagazineRepository _repository;
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<MagazineModel> _logger;
        /// <summary>
        /// Constructor with magazine Repository and logger
        /// </summary>
        /// <param name="bookRepository"></param>
        /// <param name="logger"></param>
        public MagazineModel( ILogger<MagazineModel> logger, IMagazineRepository repository )
        {
            _logger = logger;
            _repository = repository;
        }
        /// <summary>
        /// Adding copy of magazine from Magazine model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.DTOs.Copy AddCopyOfMagazineById(int id, bool isAvailable)
        {
            _logger.LogInformation($"Add copy of a magazine by id  from magazine model: CopyID {id}  ");
            var result = _repository?.AddCopyOfMagazineById(id, isAvailable);
            if (result == null)
            {
                return result;
            }
            return result;
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
            var result = _repository.AddCopyOfMagazineByIdResponse(id);
            if (result == null)
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// Adding  magazine from Magazine model
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.DTOs.Magazine AddMagazine(Domain.DTOs.Magazine magazine)
        {
            _logger.LogInformation($"Adding magazine from magazine model {magazine}");
            var result = _repository.AddMagazine(magazine);
            if (result == null)
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// Deleting magazine from Magazine model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.DTOs.Magazine DeleteMagazine(int id)
        {
            _logger.LogInformation($"Deleting magazine from magazine model: MagazineID {id}");
            var result = _repository.DeleteMagazine(id);
            if (result == null)
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// Get all magazines from Magazine model
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<Domain.DTOs.Magazine> GetAllMagazines()
        {
            _logger.LogInformation($"Getting all magazines from magazine model ");
           var magazines =  _repository.GetAllMagazines();
            if (magazines.Any())
            {
                return magazines;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Get all magazines for response
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<Domain.DTOs.Magazine> GetAllMagazinesResponse()
        {
            _logger.LogInformation("Get all magazines for response DTO from magazine model  ");
            var result = _repository.GetAllMagazinesResponse();
            if (result == null)
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// Get magazine by id from magazine model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.DTOs.Magazine GetMagazineById(int id)
        {
            _logger.LogInformation($"Getting magazine by id from magazine model: MagazineID {id} ");
            var result = _repository.GetMagazineById(id);
            if (result == null)
            {
                return result;
            }
            return result;
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
            var result = _repository.GetMagazineByIdResponse(id);
            if (result == null)
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// Update magazine from book model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.DTOs.Magazine UpdateMagazine(int id, Domain.DTOs.Magazine magazine)
        {
            _logger.LogInformation($"Updating magazine from magazine model: MagazineID {id}");
            var result = _repository.UpdateMagazine(id, magazine);
            if (result == null)
            {
                return result;
            }
            return result;
        }
    }
}
