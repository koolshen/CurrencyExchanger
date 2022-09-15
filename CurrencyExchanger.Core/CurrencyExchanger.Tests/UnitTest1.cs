using CurrencyExchanger.Core;
using CurrencyExchanger.Data.Enums;
using CurrencyExchanger.Data.Models;
using CurrencyExchanger.Infrastructure.Exceptions;
using CurrencyExchanger.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System.Globalization;

namespace CurrencyExchanger.Tests
{
    public class Tests
    {
        private new Dictionary<string, decimal> exchangeRates = new Dictionary<string, decimal>()
                  {
                       { "CNY/EUR" , 0.14M} ,
                       { "CNY/RUR" , 8.58M} ,
                       { "CNY/USD" , 0.14M} ,
                       { "EUR/CNY" , 6.95M} ,
                       { "EUR/RUR" , 59.6M} ,
                       { "EUR/USD" , 1.12M} ,
                       { "RUR/CNY" , 0.12M} ,
                       { "RUR/EUR" , 0.017M} ,
                       { "RUR/USD" , 0.017M} ,
                       { "USD/CNY" , 6.96M} ,
                       { "USD/EUR" , 1.12M} ,
                       { "USD/RUR" , 59.7M }
                  };

        private IServiceProvider serviceProvider;

        [SetUp]
        public void Setup()
        {
            this.serviceProvider = new ServiceCollection()
              .AddSingleton<IExchangeService, ExchangeService>(serviceProvider =>
              {
                  var logger = Mock.Of<ILogger>();
                  return new ExchangeService(logger, exchangeRates);
              })
              .AddSingleton<IWalletService, WalletService>(serviceProvider =>
              {
                  var logger = Mock.Of<ILogger>();
                  var exchangeService = new ExchangeService(logger, exchangeRates);
                  return new WalletService(logger, exchangeService);
              }).BuildServiceProvider();
        }

        [Test]
        public void Test1()
        {
            var walletService = serviceProvider.GetRequiredService<IWalletService>();
            Assert.IsNotNull(walletService);

            var eurAccount = new Account(CurrencyCode.EUR, 1, 1000);
            walletService.AddAccount(eurAccount);
            Assert.IsNotNull(eurAccount);

            var rurAccount = new Account(CurrencyCode.RUR, 2, 10000);
            walletService.AddAccount(rurAccount);
            Assert.IsNotNull(rurAccount);

            walletService.InitTransferTransaction(eurAccount.Number, rurAccount.Number, 450);
            walletService.InitTransferTransaction(eurAccount.Number, rurAccount.Number, 450);

            Assert.IsTrue(rurAccount.Amount == 63640);
            Assert.IsTrue(eurAccount.Amount == 100);

            Assert.Catch<InsufficientFundsException>(() =>
            {
                walletService.InitTransferTransaction(eurAccount.Number, rurAccount.Number, 350);
            });
        }

        [Test]
        public void Test2()
        {
            var walletService = serviceProvider.GetRequiredService<IWalletService>();
            Assert.IsNotNull(walletService);

            var rurAccount = new Account(CurrencyCode.RUR, 1, 10000);
            walletService.AddAccount(rurAccount);
            Assert.IsNotNull(rurAccount);

            var eurAccount = new Account(CurrencyCode.EUR, 2, 1000);
            walletService.AddAccount(eurAccount);
            Assert.IsNotNull(eurAccount);

            walletService.InitTransferTransaction(rurAccount.Number, eurAccount.Number, 3500);
            walletService.InitTransferTransaction(rurAccount.Number, eurAccount.Number, 3500);

            Assert.IsTrue(rurAccount.Amount == 3000);
            Assert.IsTrue(eurAccount.Amount == 1119M);

        }
    }
}