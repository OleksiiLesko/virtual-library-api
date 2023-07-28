using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Domain.DTOs;
using VirtualLibraryAPI.Repository;

namespace VirtualLibraryAPI.Models
{
    /// <summary>
    /// Client model
    /// </summary>
    public class ClientModel : IClientModel
    {
        /// <summary>
        /// Using article interface
        /// </summary>
        private readonly IClientRepository _repository;
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<ClientModel> _logger;

        /// <summary>
        /// Constructor with article Repository and logger
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        public ClientModel(ILogger<ClientModel> logger, IClientRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        /// <summary>
        /// Add client from model
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public Client AddClient(Client client)
        {
            _logger.LogInformation($"Adding client from Client model {client}");
            var result = _repository.AddClient(client);
            if (result == null)
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// Delete client from model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Client DeleteClient(int id)
        {
            _logger.LogInformation($"Deleting client from Client model: ClientID {id}");
            var result = _repository.DeleteClient(id);
            if (result == null)
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// Get all clients from model
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<Client> GetAllClients()
        {
            _logger.LogInformation($"Getting all clients from Client model ");
            var result = _repository.GetAllClients();
            if (result.Any())
            {
                return result;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Get all clients for response
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Client> GetAllClientsResponse()
        {
            _logger.LogInformation("Get all clients for response DTO from client model  ");
            var result = _repository.GetAllClientsResponse();
            if (result == null)
            {
                return result;
            }
            return result;
        }

        /// <summary>
        /// Get clients by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Client GetClientById(int id)
        {
            _logger.LogInformation($"Getting client by id from Client model: ClientID {id} ");
            var result = _repository.GetClientById(id);
            if (result == null)
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// Get user by id for response
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Client GetClientByIdResponse(int id)
        {
            _logger.LogInformation($"Get client by id for response DTO from Client model: ClientID {id}");
            var result = _repository.GetClientByIdResponse(id);
            if (result == null)
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// Update client data by model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public Client UpdateClient(int id, Client client)
        {
            _logger.LogInformation($"Updating client from Client model: ClientID {id}");
            var result = _repository.UpdateClient(id, client);
            if (result == null)
            {
                return result;
            }
            return result;
        }
    }
}
