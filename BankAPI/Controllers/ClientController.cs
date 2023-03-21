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
        private readonly ClientService _clientService;
        public ClientController(ClientService clientServiceDB)
        {
            _clientService = clientServiceDB;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDB))]
        public async Task<IActionResult> GetClient(Guid id)
        {
            return Ok(await _clientService.GetClientAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddClient(Client client)
        {
            await _clientService.AddClientAsync(client);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClient(Guid Id)
        {
            await _clientService.DeleteClientAsync(Id);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClient(Client client)
        {
            await _clientService.UpdateClientAsync(client);
            return NoContent();
        }
    }
}
