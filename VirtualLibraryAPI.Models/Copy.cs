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
    /// Model for copy
    /// </summary>
    public class Copy : ICopy
    {
        /// <summary>
        /// Using book repository
        /// </summary>
        private readonly ICopy _copyRepository;
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<Copy> _logger;

        /// <summary>
        /// Constructor with book Repository and logger
        /// </summary>
        /// <param name="bookRepository"></param>
        /// <param name="logger"></param>
        public Copy(ICopy copyRepository, ILogger<Copy> logger)
        {
            _copyRepository = copyRepository;
            _logger = logger;
        }
        public Domain.Entities.Copy AddCopy(Domain.DTOs.Copy book)
        {
            throw new NotImplementedException();
        }

        public Domain.Entities.Copy DeleteCopy(int id)
        {
            _logger.LogInformation($"Deleting copy from Copy model: CopyID {id}");
            return _copyRepository.DeleteCopy(id);
        }

        public IEnumerable<Domain.DTOs.Copy> GetAllBooksResponse()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Domain.Entities.Copy> GetAllCopies()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Domain.DTOs.Copy> GetAllCopiesResponse()
        {
            throw new NotImplementedException();
        }

        public Domain.DTOs.Copy GetCopyByIdResponse(int id)
        {
            _logger.LogInformation($"Getting copy by id for response from Copy model: CopyID {id}");
            return _copyRepository.GetCopyByIdResponse(id);
        }

        public Domain.Entities.Copy GetCopyById(int id)
        {
            _logger.LogInformation($"Getting copy by id from Copy model: CopyID {id}");
            return _copyRepository.GetCopyById(id);
        }

        public Domain.Entities.Copy UpdateCopy(int id, Domain.DTOs.Copy copy)
        {
            _logger.LogInformation($"Updating copy by id from Copy model: CopyID {id}");
            return _copyRepository.UpdateCopy(id, copy);
        }
    }
}
