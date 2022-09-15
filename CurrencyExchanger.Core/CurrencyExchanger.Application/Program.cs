using CurrencyExchanger.Core;
using CurrencyExchanger.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Globalization;
using CurrencyExchanger.Infrastructure.Exceptions;
using CurrencyExchanger.Data.Models;
using CurrencyExchanger.Data.Enums;

public partial class Program
{
    private static void Main()
    {
        var config = GetConfiguration();
        var serviceProvider = ConfigureServices(config);
        var logger = GetLogger(serviceProvider);
        logger.LogInformation("Application started");

        var walletService = serviceProvider.GetRequiredService<IWalletService>();

        var account1 = new Account(CurrencyCode.EUR, 1, 1000);
        var account2 = new Account(CurrencyCode.RUR, 2, 10000);

        walletService.AddAccount(account1);
        walletService.AddAccount(account2);

        walletService.InitTransferTransaction(1, 2, 100);
        walletService.InitTransferTransaction(1, 2, 100);
        walletService.InitTransferTransaction(1, 2, 100);
        walletService.InitTransferTransaction(1, 2, 100);
        walletService.InitTransferTransaction(1, 2, 100);
        walletService.InitTransferTransaction(1, 2, 100);
        walletService.InitTransferTransaction(1, 2, 100);
        walletService.InitTransferTransaction(1, 2, 100);
        walletService.InitTransferTransaction(1, 2, 100);
        walletService.InitTransferTransaction(1, 2, 100);
        walletService.InitTransferTransaction(1, 2, 100);




        Console.WriteLine();
    }

    #region Init Services
    private static ServiceProvider ConfigureServices(IConfiguration config)
    {
        try
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IExchangeService, ExchangeService>(serviceProvider =>
                {
                    var exchangeRates = config.GetSection("ExchangeRates").GetChildren().ToDictionary(x => x.Key, x => decimal.Parse(x.Value, CultureInfo.InvariantCulture));
                    var logger = GetLogger(serviceProvider);
                    return new ExchangeService(logger, exchangeRates);
                })
                .AddSingleton<IWalletService, WalletService>(serviceProvider =>
                {
                    var logger = GetLogger(serviceProvider);
                    var exchangeService = GetExchangeService(serviceProvider);
                    return new WalletService(logger, exchangeService);
                })
                .BuildServiceProvider();

            return serviceProvider;
        }
        catch (Exception ex)
        {
            throw new ApplicationInitializationException(ex.Message);
        }
    }
    #endregion

    #region Init IConfiguration
    private static IConfiguration GetConfiguration()
    {
        try
        {
            return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
        }
        catch (Exception ex)
        {
            throw new ApplicationInitializationException(ex.Message);
        }
    }
    #endregion

    #region Init Logger
    private static ILogger GetLogger(IServiceProvider serviceProvider)
    {
        var logger = serviceProvider.GetService<ILoggerFactory>()?.CreateLogger<Program>();

        if (logger == null)
        {
            throw new ApplicationInitializationException("Error during Init Logger Service");
        }

        return logger;
    }
    #endregion

    #region Init Exchange Service
    private static IExchangeService GetExchangeService(IServiceProvider serviceProvider)
    {
        var exchangeService = serviceProvider.GetService<IExchangeService>();

        if (exchangeService == null)
        {
            throw new ApplicationInitializationException("Error during Init IExchangeService");
        }

        return exchangeService;
    }
    #endregion
}