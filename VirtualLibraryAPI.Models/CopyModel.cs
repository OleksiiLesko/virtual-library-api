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
    public class CopyModel : ICopyModel
    {
        /// <summary>
        /// Using book repository
        /// </summary>
        private readonly ICopyRepository _repository;
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<CopyModel> _logger;

        /// <summary>
        /// Constructor with book Repository and logger
        /// </summary>
        /// <param name="bookRepository"></param>
        /// <param name="logger"></param>
        public CopyModel(ILogger<CopyModel> logger, ICopyRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public Domain.DTOs.Copy DeleteCopy(int id)
        {
            _logger.LogInformation($"Deleting copy from Copy model: CopyID {id}");
            var result = _repository.DeleteCopy(id);
            if (result == null)
            {
                return result;
            }
            return result;
        }

        public Domain.DTOs.Copy GetCopyByIdResponse(int id)
        {
            _logger.LogInformation($"Getting copy by id for response from Copy model: CopyID {id}");
            var result = _repository.GetCopyByIdResponse(id);
            if (result == null)
            {
                return result;
            }
            return result;
        }

        public Domain.DTOs.Copy GetCopyById(int id)
        {
            _logger.LogInformation($"Getting copy by id from Copy model: CopyID {id}");
            var result = _repository.GetCopyById(id);
            if (result == null)
            {
                return result;
            }
            return result;
        }

        public Domain.DTOs.Copy UpdateCopy(int id, Domain.DTOs.Copy copy)
        {
            _logger.LogInformation($"Updating copy by id from Copy model: CopyID {id}");
            var result = _repository.UpdateCopy(id, copy);
            if (result == null)
            {
                return result;
            }
            return result;
        }
    }
}
