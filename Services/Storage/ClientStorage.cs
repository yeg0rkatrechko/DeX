using Bogus.DataSets;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Storage
{
    internal class ClientStorage
    {
        public Dictionary<Client, List<Account>> Data { get; }
        public void Add(Client client)
        {
            Data.Add(
               client,
               new List<Account>
               {
                    new Account
                    {
                        Currency = new Models.Currency("USD", 1),
                        Amount = 0
                    }
               });
        }
        public void AddAccount(Client client, Account account)
        {
            Data[client].Add(account);
        }
        public void Remove(Client client) { throw new NotImplementedException(); }
        public void RemoveAccount(Client client, Account account) { throw new NotImplementedException(); }

        //public void Update

    }
}
