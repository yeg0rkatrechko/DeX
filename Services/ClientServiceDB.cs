using Bogus.DataSets;
using DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Models;
using Services.Exceptions;
using Services.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ClientServiceDB
    {
        public BankContext dbContext;
        public ClientServiceDB()
        {
            dbContext = new BankContext();
        }
        public ClientDB GetClient(Guid clientID)
        {
            var client = dbContext.Clients.FirstOrDefault(c => c.Id == clientID);

            if (client == null)
            {
                throw new ExistenceException("Этого клиента не сущетсвует");
            }
            return dbContext.Clients.FirstOrDefault(c => c.Id == clientID);
        }
        public async Task<ClientDB> GetClientAsync(Guid clientID)
        {

            var client = await dbContext.Clients.FirstOrDefaultAsync(c => c.Id == clientID);

            return dbContext.Clients.FirstOrDefault(c => c.Id == clientID);
        }
        public ClientDB GetClientByPassID(string clientPassID)
        {
            var client = dbContext.Clients.FirstOrDefault(c => c.PassportID == clientPassID);

            if (client == null)
            {
                throw new ExistenceException("Этого клиента не сущетсвует");
            }
            return dbContext.Clients.FirstOrDefault(c => c.PassportID == clientPassID);
        }
        public List<Client> GetClients(ClientsFilter clientFilter)
        {
            var selection = dbContext.Employees.Select(p => p);

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
            dbContext.Clients.Add(clientDb);
            dbContext.Account.Add(new AccountDB
            {
                Amount = 0,
                ClientId = clientDb.Id,
                Currency = new CurrencyDB
                {
                    Name = "USD",
                    Code = 1
                }
            });
            dbContext.SaveChanges();
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

            if (await dbContext.Clients.FirstOrDefaultAsync(p => p.Id == client.ID) != null)
                throw new ArgumentException("Такой клиент уже существует");

            await dbContext.Clients.AddAsync(clientDb);

            await dbContext.Account.AddAsync(new AccountDB
            {
                Amount = 0,
                ClientId = client.ID,
                Currency = new CurrencyDB
                {
                    Name = "USD",
                    Code = 1
                }
            });

            await dbContext.SaveChangesAsync();
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
            dbContext.Account.Add(accountDb);
            dbContext.SaveChanges();
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

            await dbContext.Account.AddAsync(accountDb);
            await dbContext.SaveChangesAsync();
        }
        public void UpdateClient(Client client)
        {
            var clientDb = dbContext.Clients.FirstOrDefault(c => c.Id == client.ID);

            if (clientDb == null)
                throw new ExistenceException("Данного клиента не существует");
            
            clientDb.Id = client.ID;
            clientDb.Name = client.Name;
            clientDb.PassportID = client.PassportID;
            clientDb.DateOfBirth = client.DateOfBirth;
            clientDb.Bonus = 0;

            dbContext.Clients.Update(clientDb);
            dbContext.SaveChanges();
        }
        public async Task UpdateClientAsync(Client client)
        {
            var clientDb = await dbContext.Clients.FirstOrDefaultAsync(p => p.Id == client.ID);

            if (await dbContext.Clients.FirstOrDefaultAsync(p => p.Id == client.ID) == null)
                throw new ExistenceException("Данного клиента не существует");
            clientDb.Id = client.ID;
            clientDb.Name = client.Name;
            clientDb.PassportID = client.PassportID;
            clientDb.DateOfBirth = client.DateOfBirth;
            clientDb.Bonus = 0;

            dbContext.Clients.Update(clientDb);
            await dbContext.SaveChangesAsync();

        }
        public void DeleteClient(Client client)
        {
            var clientDb = dbContext.Clients.FirstOrDefault(c => c.Id == client.ID);
            if (clientDb == null)
            {
                throw new ExistenceException("Клиент с данным ID отсутствует в базе");
            }
            dbContext.Clients.Remove(clientDb);
            dbContext.SaveChanges();
        }
        public async Task DeleteClientAsync(Client client)
        {
            var clientDb = await dbContext.Clients.FirstOrDefaultAsync(c => c.Id == client.ID);

            if (clientDb == null)
            {
                throw new KeyNotFoundException("В базе нет такого клиента");
            }
            dbContext.Clients.Remove(clientDb);
            await dbContext.SaveChangesAsync();

        }
        public void DeleteAccount(Client client)
        {
            var clientDb = dbContext.Clients.FirstOrDefault(c => c.Id == client.ID);
            if (clientDb == null)
            {
                throw new ExistenceException("Клиент с данным ID отсутствует в базе");
            }
            var accountDb = dbContext.Account.FirstOrDefault(a => a.ClientId == client.ID);
            dbContext.Account.Remove(accountDb);
            dbContext.SaveChanges();
        }
        public async Task UpdateAccountAsync(Guid id, Account account)
        {
            if (await dbContext.Clients.FirstOrDefaultAsync(p => p.Id == id) == null)
                throw new KeyNotFoundException("Этого клиента не сущетсвует");

            var accountDb = await dbContext.Account.FirstOrDefaultAsync(p => p.ClientId == id/* && p =>*/);

            //if (accountDb == null)
            //    throw new NullReferenceException("У клиента нет такого счета");

            accountDb.Amount = account.Amount;

            dbContext.Account.Update(accountDb);
            await dbContext.SaveChangesAsync();

        }
    }
}
