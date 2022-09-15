using CurrencyExchanger.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchanger.Infrastructure.Interfaces
{
    public interface IWalletService
    {
        public void AddAccount(Account account);
        public void InitTransferTransaction(int fromAccountNumber, int toAccountNumber, decimal amount);
    }
}
