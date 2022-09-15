using CurrencyExchanger.Data.Models;
using CurrencyExchanger.Infrastructure.Exceptions;
using CurrencyExchanger.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchanger.Core
{
    public class WalletService : IWalletService
    {
        private readonly IExchangeService exchangeService;
        private readonly ILogger logger;

        public WalletService(ILogger logger, IExchangeService exchangeService)
        {
            this.exchangeService = exchangeService;
            this.logger = logger;
        }

        public HashSet<Account> Accounts { get; set; } = new HashSet<Account>();

        public void AddAccount(Account account)
        {
            logger.LogInformation($"Adding new account to the wallet with paramwters: Number : {account.Number}, Currency {account.Code}, Amount : {account.Amount}");

            if(Accounts.Any(x => x.Number == account.Number))
            {
                throw new InvalidOperationException("Can not insert account with same number");
            } 

            Accounts.Add(account);
        }

        public void InitTransferTransaction(int fromAccountNumber, int toAccountNumber, decimal amount)
        {
            var fromAccount = Accounts.FirstOrDefault(x => x.Number == fromAccountNumber);
            var toAccount = Accounts.FirstOrDefault(x => x.Number == toAccountNumber);
            ValidateParameters(fromAccount, toAccount);

            exchangeService.ExchangeCurrency(fromAccount, toAccount, amount);

        }

        private void ValidateParameters(Account fromAccount, Account toAccount)
        {
            if (fromAccount == null || toAccount == null)
            {
                throw new AccountNotFoundException($"No such accounts");
            }
        }
    }
}
