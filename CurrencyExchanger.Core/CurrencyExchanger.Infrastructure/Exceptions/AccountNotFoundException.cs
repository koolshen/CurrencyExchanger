namespace CurrencyExchanger.Infrastructure.Exceptions
{
    public class AccountNotFoundException : ApplicationException
    {
        public AccountNotFoundException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
