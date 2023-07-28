using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLibraryAPI.Models
{
    /// <summary>
    /// Interface for client
    /// </summary>
    public interface IClientModel
    {
        /// <summary>
        /// Method for get all clients
        /// </summary>
        /// <returns></returns>
        IEnumerable<Domain.DTOs.Client> GetAllClients();
        /// <summary>
        /// Method for get client by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.DTOs.Client GetClientById(int id);
        /// <summary>
        /// Method for add client
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        Domain.DTOs.Client AddClient(Domain.DTOs.Client client);
        /// <summary>
        /// Method for delete client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.DTOs.Client DeleteClient(int id);
        /// <summary>
        /// Get all clients for response DTO
        /// </summary>
        /// <returns></returns>
        IEnumerable<Domain.DTOs.Client> GetAllClientsResponse();
        /// <summary>
        /// Get  client by id for response DTO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.DTOs.Client GetClientByIdResponse(int id);
        /// <summary>
        /// Method for update client data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        Domain.DTOs.Client UpdateClient(int id, Domain.DTOs.Client client);
    }
}
