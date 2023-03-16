using AutoMapper;
using DbModels;
using Microsoft.EntityFrameworkCore;
using Models;
using Services.Exceptions;
using DbModels.Dtos;

namespace Services
{
    public class ClientService
    {
        private Dictionary<Client, List<Account>> tempStorage;
        public readonly IMapper _mapper;
        public readonly BankContext _dbContext;
        public ClientService(IMapper mapper, BankContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<ClientDto> GetClientAsync(Guid clientID)
        {
            var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == clientID);

            var response = _mapper.Map<ClientDto>(client);

            return response;
        }
        public async Task AddClientAsync(Client client)
        {
            var clientDb = new ClientDB
            {
                Id = client.ID,
                Name = client.Name,
                PassportID = client.PassportID,
                DateOfBirth = client.DateOfBirth,
                Bonus = 0,
            };

            if ((DateTime.Now.Year - clientDb.DateOfBirth.Year) < 18)
                throw new Limit18YearsException("Клиент не может быть младше 18 лет");

            if (clientDb.PassportID == null)
                throw new NoPassportDataException("Вы не ввели паспортные данные");

            if (await _dbContext.Clients.FirstOrDefaultAsync(p => p.Id == client.ID) != null)
                throw new ArgumentException("Такой клиент уже существует");

            await _dbContext.Clients.AddAsync(clientDb);

            await _dbContext.Account.AddAsync(new AccountDB
            {
                Amount = 0,
                ClientId = client.ID,
                Currency = new CurrencyDB
                {
                    Name = "USD",
                    Code = 1
                }
            });

            await _dbContext.SaveChangesAsync();
        }
        public async Task AddAccountAsync(Guid id, Account account)
        {
            var accountDb = new AccountDB
            {
                Amount = account.Amount,
                ClientId = id,
                Currency = new CurrencyDB
                {
                    Name = "USD",
                    Code = 1,
                }
            };

            await _dbContext.Account.AddAsync(accountDb);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateClientAsync(Client client)
        {
            var clientDb = await _dbContext.Clients.FirstOrDefaultAsync(p => p.Id == client.ID);

            if (await _dbContext.Clients.FirstOrDefaultAsync(p => p.Id == client.ID) == null)
                throw new AccountAlreadyExistsException("Данного клиента не существует");
            clientDb.Id = client.ID;
            clientDb.Name = client.Name;
            clientDb.PassportID = client.PassportID;
            clientDb.DateOfBirth = client.DateOfBirth;
            clientDb.Bonus = 0;

            _dbContext.Clients.Update(clientDb);
            await _dbContext.SaveChangesAsync();

        }
        public async Task DeleteClientAsync(Guid ClientId)
        {
            var clientDb = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == ClientId);

            if (clientDb == null)
            {
                throw new KeyNotFoundException("В базе нет такого клиента");
            }
            _dbContext.Clients.Remove(clientDb);
            await _dbContext.SaveChangesAsync();

        }
        public async Task UpdateAccountAsync(Guid id, Account account)
        {
            if (await _dbContext.Clients.FirstOrDefaultAsync(p => p.Id == id) == null)
                throw new KeyNotFoundException("Этого клиента не сущетсвует");

            var accountDb = await _dbContext.Account.FirstOrDefaultAsync(p => p.ClientId == id/* && p =>*/);

            //if (accountDb == null)
            //    throw new NullReferenceException("У клиента нет такого счета");

            accountDb.Amount = account.Amount;

            _dbContext.Account.Update(accountDb);
            await _dbContext.SaveChangesAsync();

        }
        public void DictionaryOutput()
        {
            foreach (var temp in tempStorage)
            {
                Console.WriteLine(tempStorage.Keys);
            }
        }
    }
}
