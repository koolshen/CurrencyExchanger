using CurrencyExchanger.Data.Models;
using CurrencyExchanger.Infrastructure.Exceptions;
using CurrencyExchanger.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace CurrencyExchanger.Core
{
    public class ExchangeService : IExchangeService
    {
        private readonly Dictionary<string, decimal> exchangeRates; 
        private readonly ILogger logger; 

        public ExchangeService(ILogger logger, Dictionary<string, decimal> exchangeRates)
        {
            this.exchangeRates = exchangeRates;
            this.logger = logger;
        }

        public void ExchangeCurrency(Account from, Account to, decimal amount)
        {

            logger.LogInformation($"Exchange {from.Code} => {to.Code} with amount {amount} started");

            try
            {
                if (from.Amount - amount < 0)
                {
                    throw new InsufficientFundsException($"InsufficientFunds");
                }

                if (!exchangeRates.ContainsKey($"{from.Code}/{to.Code}"))
                {
                    throw new ExchangeOperationNotSupportedException($"{from.Code}/{to.Code} exchange not supported");
                }

                var rate = exchangeRates[$"{from.Code}/{to.Code}"];

                var value = amount * rate;

                to.Amount += value;

                from.Amount -= amount;

                logger.LogInformation($"Exchange {from.Code} => {to.Code} with amount {amount} completed successfuly");
            }
            catch (InsufficientFundsException ex)
            {
                logger.LogCritical(ex.Message);
                throw new InsufficientFundsException(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogCritical($"Exchange {from.Code} => {to.Code} with amount {amount} completed with error", ex.Message);
                throw new ExchangeErrorException(ex.Message);
            }
        }
    }
}