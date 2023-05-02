using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLibraryAPI.Repository
{
    public interface ICopy
    {
        /// <summary>
        /// Method for get all copies
        /// </summary>
        /// <returns></returns>
        IEnumerable<Domain.Entities.Copy> GetAllCopies();
        /// <summary>
        /// Method for get copy by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.Entities.Copy GetCopyById(int id);
        /// <summary>
        /// Method for add copy
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        Domain.Entities.Copy AddCopy(Domain.DTOs.Book book);
        /// <summary>
        /// Method for update copy
        /// </summary>
        /// <param name="id"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        Domain.Entities.Copy UpdateCopy(int id, Domain.DTOs.Book book);
        /// <summary>
        /// Method for delete copy
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.Entities.Copy DeleteCopy(int id);
        /// <summary>
        /// Get  book by id for response DTO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.DTOs.Book GetBookByIdResponse(int id);
        /// <summary>
        /// Get all books for response DTO
        /// </summary>
        /// <returns></returns>
        IEnumerable<Domain.DTOs.Book> GetAllBooksResponse();
    }
}
