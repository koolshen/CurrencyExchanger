namespace CurrencyExchanger.Infrastructure.Exceptions
{
    public class InsufficientFundsException : ApplicationException
    {
        public InsufficientFundsException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
