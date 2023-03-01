using DbModels;
using Microsoft.EntityFrameworkCore;
using Models;
using Services.Exceptions;
using Services.Filters;

namespace Services
{
    public class ClientServiceDB
    {
        private readonly BankContext _dbContext;
        public ClientServiceDB(BankContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ClientDB GetClient(Guid clientID)
        {
            var client = _dbContext.Clients.FirstOrDefault(c => c.Id == clientID);

            if (client == null)
            {
                throw new ExistenceException("Этого клиента не сущетсвует");
            }
            return _dbContext.Clients.FirstOrDefault(c => c.Id == clientID);
        }
        public async Task<ClientDB> GetClientAsync(Guid clientID)
        {

            var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == clientID);

            return _dbContext.Clients.FirstOrDefault(c => c.Id == clientID);
        }
        public ClientDB GetClientByPassID(string clientPassID)
        {
            var client = _dbContext.Clients.FirstOrDefault(c => c.PassportID == clientPassID);

            if (client == null)
            {
                throw new ExistenceException("Этого клиента не сущетсвует");
            }
            return _dbContext.Clients.FirstOrDefault(c => c.PassportID == clientPassID);
        }
        public List<Client> GetClients(ClientsFilter clientFilter)
        {
            var selection = _dbContext.Employees.Select(p => p);

            if (clientFilter.Name != null)
                selection = selection.
                    Where(p => p.Name == clientFilter.Name);

            if (clientFilter.PassportID != null)
                selection = selection.
                   Where(p => p.PassportID == clientFilter.PassportID);

            if (clientFilter.StartDate != new DateTime())
                selection = selection.
                   Where(p => p.DateOfBirth == clientFilter.StartDate);

            if (clientFilter.EndDate != new DateTime())
                selection = selection.
                   Where(p => p.DateOfBirth == clientFilter.EndDate);

            return selection.Select(clientDb => new Client()
            {
                ID = clientDb.Id,
                Name = clientDb.Name,
                PassportID = clientDb.PassportID,
                DateOfBirth = clientDb.DateOfBirth
            })
            .ToList();
        }
        public void AddClient(Client client)
        {
            var clientDb = new ClientDB()
            {
                Id = client.ID,
                Name = client.Name,
                PassportID = client.PassportID,
                DateOfBirth = client.DateOfBirth
            };
            if (clientDb.PassportID == null)
            {
                throw new NoPassData("Вы не ввели паспортные данные");
            }

            if (DateTime.Now.Year - clientDb.DateOfBirth.Year < 18)
            {
                throw new Under18("Клиент не может быть младше 18-ти лет");
            }
            _dbContext.Clients.Add(clientDb);
            _dbContext.Account.Add(new AccountDB
            {
                Amount = 0,
                ClientId = clientDb.Id,
                Currency = new CurrencyDB
                {
                    Name = "USD",
                    Code = 1
                }
            });
            _dbContext.SaveChanges();
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
                throw new Under18("Клиент не может быть младше 18 лет");

            if (clientDb.PassportID == null)
                throw new NoPassData("Вы не ввели паспортные данные");

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
        public void AddAccount(Guid clientID) 
        {
            var accountDb = new AccountDB()
            {
                Amount = 0,
                ClientId = clientID,
                Currency = new CurrencyDB
                {
                    Name = "USD",
                    Code = 1
                }
            };
            _dbContext.Account.Add(accountDb);
            _dbContext.SaveChanges();
        }
        public void UpdateClient(Client client)
        {
            var clientDb = _dbContext.Clients.FirstOrDefault(c => c.Id == client.ID);

            if (clientDb == null)
            {
                throw new ExistenceException("Данного клиента не существует");
            }
            clientDb.Id = client.ID;
            clientDb.Name = client.Name;
            clientDb.PassportID = client.PassportID;
            clientDb.DateOfBirth = client.DateOfBirth;
            clientDb.Bonus = 0;

            _dbContext.Clients.Update(clientDb);
            _dbContext.SaveChanges();
        }
        public async Task UpdateClientAsync(Client client)
        {
            var clientDb = await _dbContext.Clients.FirstOrDefaultAsync(p => p.Id == client.ID);

            if (clientDb == null)
                throw new KeyNotFoundException("Данного клиента не существует");
            clientDb.Id = client.ID;
            clientDb.Name = client.Name;
            clientDb.PassportID = client.PassportID;
            clientDb.DateOfBirth = client.DateOfBirth;
            clientDb.Bonus = 0;

            _dbContext.Clients.Update(clientDb);
            await _dbContext.SaveChangesAsync();

        }
        public void DeleteClient(Client client)
        {
            var clientDb = _dbContext.Clients.FirstOrDefault(c => c.Id == client.ID);
            if (clientDb == null)
            {
                throw new ExistenceException("Клиент с данным ID отсутствует в базе");
            }
            _dbContext.Clients.Remove(clientDb);
            _dbContext.SaveChanges();
        }
        public async Task DeleteClientAsync(Client client)
        {
            var clientDb = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == client.ID);

            if (clientDb == null)
            {
                throw new KeyNotFoundException("В базе нет такого клиента");
            }
            _dbContext.Clients.Remove(clientDb);
            await _dbContext.SaveChangesAsync();

        }
        public void DeleteAccount(Client client)
        {
            var clientDb = _dbContext.Clients.FirstOrDefault(c => c.Id == client.ID);
            if (clientDb == null)
            {
                throw new ExistenceException("Клиент с данным ID отсутствует в базе");
            }
            var accountDb = _dbContext.Account.FirstOrDefault(a => a.ClientId == client.ID);
            _dbContext.Account.Remove(accountDb);
            _dbContext.SaveChanges();
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
    }
}
