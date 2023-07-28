using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Domain;

namespace VirtualLibraryAPI.Repository.Repositories
{
    /// <summary>
    /// Client repository 
    /// </summary>
    public class Client : IClientRepository
    {
        /// <summary>
        /// Application context
        /// </summary>
        private readonly ApplicationContext _context;
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<Client> _logger;
        /// <summary>
        /// Constructor with context and logger
        /// </summary>
        /// <param name="context"></param>
        public Client(ApplicationContext context, ILogger<Client> logger)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// Add client to the database
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public Domain.DTOs.Client AddClient(Domain.DTOs.Client client)
        {
            var newClient = new Domain.Entities.Client()
            {
                FirstName = client.FirstName,
                LastName = client.LastName
            };

            _context.Clients.Add(newClient);
            _context.SaveChanges();

            _logger.LogInformation("Adding client to the database: {ClientID}", newClient.ClientID);

            var addedClient = new Domain.DTOs.Client
            {
                ClientID = newClient.ClientID,
                FirstName = newClient.FirstName,
                LastName = newClient.LastName
            };

            return addedClient;
        }
        /// <summary>
        /// Delete client from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.DTOs.Client DeleteClient(int id)
        {
            var client = _context.Clients.Find(id);

            _context.Clients.Remove(client);
            _context.SaveChanges();
            var deletedClientDto = new Domain.DTOs.Client
            {
                ClientID = client.ClientID,
                FirstName = client.FirstName,
                LastName = client.LastName,
            };
            _logger.LogInformation("Deleting client from database: {ClientID}", client.ClientID);

            return deletedClientDto;
        }
        /// <summary>
        /// Get all clients from database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<Domain.DTOs.Client> GetAllClients()
        {
            var clientEntities = _context.Clients.ToList();
            var clientDtos = new List<Domain.DTOs.Client>();

            foreach (var clientEntity in clientEntities)
            {
                var clientDto = new Domain.DTOs.Client();

                clientDtos.Add(clientDto);
            }
            _logger.LogInformation("Returning all clients from the database");


            return clientDtos;
        }
        /// <summary>
        /// Get client by id from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.DTOs.Client GetClientById(int id)
        {
            _logger.LogInformation($"Getting client id: ClientID {id}");

            var clientEntity = _context.Clients.FirstOrDefault(b => b.ClientID == id);
            if (clientEntity == null)
            {
                return null;
            }
            var clientDto = new Domain.DTOs.Client
            {
                ClientID = clientEntity.ClientID,
                FirstName = clientEntity.FirstName,
                LastName = clientEntity.LastName,
            };

            return clientDto;
        }
        /// <summary>
        /// Get all clients for response
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Domain.DTOs.Client> GetAllClientsResponse()
        {
            _logger.LogInformation("Get all clients for response DTO:");

            var clients = _context.Clients
                .Select(x => new Domain.DTOs.Client
                {
                    ClientID = x.ClientID,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                }).ToList();
            return clients;
        }
        /// <summary>
        /// Get client by id for response
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>

        public Domain.DTOs.Client GetClientByIdResponse(int id)
        {
            var result = _context.Clients
             .FirstOrDefault(x => x.ClientID == id);

            _logger.LogInformation($"Get client by id for response: UserID {id}");

            var clientDTO = new Domain.DTOs.Client
            {
                ClientID = result.ClientID,
                FirstName = result.FirstName,
                LastName = result.LastName,
            };
            return clientDTO;
        }
        /// <summary>
        /// Update client data 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.DTOs.Client UpdateClient(int id, Domain.DTOs.Client client)
        {
            var existingClient = _context.Clients.Find(id);

            existingClient.FirstName = client.FirstName;
            existingClient.LastName = client.LastName;

            _context.SaveChanges();
            _logger.LogInformation("Update client by id in the database: {ClientID}", existingClient.ClientID);

            return client;
        }
        /// <summary>
        /// Count client copies
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public int CountClientCopies(int clientId)
        {
            var client = _context.Clients
                .Include(u => u.Copies)
                .FirstOrDefault(u => u.ClientID == clientId);

            if (client == null)
            {
                _logger.LogInformation($"ClientID: {clientId} not found");
                return 0;
            }

            return client.Copies.Count;
        }
        /// <summary>
        /// Check if client have expired copies
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public bool HasExpiredCopy(int clientId)
        {
            var client = _context.Clients.FirstOrDefault(u => u.ClientID == clientId);
            if (client == null)
            {
                _logger.LogInformation($"ClientID: {clientId} not found");
                return false;
            }
            if (client.Copies == null)
            {
                return false;
            }
            foreach (var copy in client.Copies)
            {
                if (copy.ExpirationDate <= DateTime.Now)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
