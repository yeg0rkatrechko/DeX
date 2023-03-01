using Models;

namespace Services.Storage
{
    internal class ClientStorage : IClientStorage
    {
        private Dictionary<Client, List<Account>> data = new Dictionary<Client, List<Account>>();
        public Dictionary<Client, List<Account>> Data => data;
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
        public void Delete(Client client)
        {
            Data.Remove(client);
        }
        public void DeleteAccount(Client client, Account account)
        {
            Data[client].Remove(account);
        }
        public void Update(Client client)
        {
            var oldClient = Data.Keys.First(p => p.PassportID == client.PassportID);

            oldClient.Name = client.Name;
            oldClient.PassportID = client.PassportID;
            oldClient.DateOfBirth = client.DateOfBirth;

        }
        public void UpdateAccount(Client client, Account account)
        {
            var oldAccount = Data[client].First(p => p.Currency.Name == account.Currency.Name);

            oldAccount.Currency = new Models.Currency
            {
                Code = account.Currency.Code,
                Name = account.Currency.Name,
            };
            oldAccount.Amount = account.Amount;

        }

    }
}
