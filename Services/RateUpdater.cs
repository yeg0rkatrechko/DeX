using Models;
using DbModels;

namespace Services
{
    public class RateUpdaterService
    {
        private ClientService _clientService;
        public RateUpdaterService(ClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task AccruingInterestAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                BankContext _bankContext = new BankContext();

                var accountsDb = _bankContext.Account.Take(10).ToList();

                foreach (var accountDb in accountsDb)
                {
                    var account = new Account
                    {
                        Amount = accountDb.Amount,
                        Currency = new Models.Currency
                        {
                            Name = "USD",
                            Code = 1
                        }
                    };

                    account.Amount += 5;
                    _clientService.UpdateAccountAsync(accountDb.ClientId, account);
                }

                Task.Delay(1000).Wait();
            }
        }
    }
}