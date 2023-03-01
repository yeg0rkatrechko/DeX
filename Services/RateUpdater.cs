using Models;
using DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bogus.DataSets;

namespace Services
{
    public class RateUpdaterService
    {
        private ClientServiceDB _clientService;
        private BankContext _dbContext;
        public RateUpdaterService(ClientServiceDB clientService, BankContext dbContext)
        {
            _clientService = clientService;
            _dbContext = dbContext;
        }

        public async Task AccruingInterestAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var accountsDb = _dbContext.Account.Take(10).ToList();

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
                    await _clientService.UpdateAccountAsync(accountDb.ClientId, account);
                }

                await Task.Delay(1000).WaitAsync(token);
            }
        }
    }
}