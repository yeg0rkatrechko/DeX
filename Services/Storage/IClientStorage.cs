using Models;

namespace Services.Storage
{
    internal interface IClientStorage : IStorage<Client>
    {
        public void AddAccount(Client client, Account account);
        public void DeleteAccount(Client client, Account account);
        public void UpdateAccount(Client client, Account account);
        public Dictionary<Client, List<Account>> Data { get; }
    }
}
