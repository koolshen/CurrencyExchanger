namespace CurrencyExchanger.Infrastructure.Exceptions
{
    public class ApplicationInitializationException : ApplicationException
    {
        public ApplicationInitializationException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
