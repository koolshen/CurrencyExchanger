using CurrencyExchanger.Data.Models;

namespace CurrencyExchanger.Infrastructure.Interfaces
{
    public interface IExchangeService
    {
        void ExchangeCurrency(Account from, Account to, decimal amount);
    }
}
