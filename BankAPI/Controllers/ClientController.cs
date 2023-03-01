using DbModels;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace BankAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private ClientServiceDB _clientService;
        public ClientController()
        {
            _clientService = new ClientServiceDB();
        }

        [HttpGet]
        public async Task<ClientDB> GetClient(Guid id)
        {
            return await _clientService.GetClientAsync(id);
        }

        [HttpPost]
        public async Task AddClient(Client client)
        {
            await _clientService.AddClientAsync(client);
        }

        [HttpDelete]
        public async Task DeleteClient(Client client)
        {
            await _clientService.DeleteClientAsync(client);
        }

        [HttpPut]
        public async Task UpdateClient(Client client)
        {
            await _clientService.UpdateClientAsync(client);
        }
    }
}
