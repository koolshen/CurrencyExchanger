namespace CurrencyExchanger.Infrastructure.Exceptions
{
    public class ExchangeOperationNotSupportedException : ApplicationException
    {
        public ExchangeOperationNotSupportedException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
