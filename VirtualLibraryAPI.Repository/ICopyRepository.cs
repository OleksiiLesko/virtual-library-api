using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Common;

namespace VirtualLibraryAPI.Repository
{
    /// <summary>
    /// Copy repository interface
    /// </summary>
    public interface ICopyRepository
    {
        /// <summary>
        /// Method for get copy by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.DTOs.Copy GetCopyById(int id);
        /// <summary>
        /// Method for update copy
        /// </summary>
        /// <param name="id"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        Domain.DTOs.Copy UpdateCopy(int id, Domain.DTOs.Copy book);
        /// <summary>
        /// Method for delete copy
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.DTOs.Copy DeleteCopy(int id);
        /// <summary>
        /// Get  book by id for response DTO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.DTOs.Copy GetCopyByIdResponse(int id);
    }
}
