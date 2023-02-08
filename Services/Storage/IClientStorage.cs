using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Storage
{
    internal interface IClientStorage : IStorage<Client>
    {
        public void AddAccount(Client client, Account account);
        public void RemoveAccount(Client client, Account account);
        public void UpdateAccount(Client client, Account account);
        public Dictionary<Client, List<Account>> Data { get; }
    }
}
