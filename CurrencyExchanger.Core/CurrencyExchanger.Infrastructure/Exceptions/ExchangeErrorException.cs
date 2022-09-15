namespace CurrencyExchanger.Infrastructure.Exceptions
{
    public class ExchangeErrorException : ApplicationException
    {
        public ExchangeErrorException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
