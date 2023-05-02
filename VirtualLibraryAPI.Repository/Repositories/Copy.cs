using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Domain;

namespace VirtualLibraryAPI.Repository.Repositories
{
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
        public Domain.Entities.Copy AddCopy(Domain.DTOs.Book book)
        {
            throw new NotImplementedException();
        }

        public Domain.Entities.Copy DeleteCopy(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Domain.DTOs.Book> GetAllBooksResponse()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Domain.Entities.Copy> GetAllCopies()
        {
            throw new NotImplementedException();
        }

        public Domain.DTOs.Book GetBookByIdResponse(int id)
        {
            throw new NotImplementedException();
        }

        public Domain.Entities.Copy GetCopyById(int id)
        {
            throw new NotImplementedException();
        }

        public Domain.Entities.Copy UpdateCopy(int id, Domain.DTOs.Book book)
        {
            throw new NotImplementedException();
        }
    }
}
