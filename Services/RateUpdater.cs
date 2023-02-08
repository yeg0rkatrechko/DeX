﻿using Models;
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
        public RateUpdaterService(ClientServiceDB clientService)
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