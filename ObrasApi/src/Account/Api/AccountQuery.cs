using ObrasApi.src.Account.Database.Domain;

namespace ObrasApi.src.Account.Api
{
    public class AccountQuery
    {
        public AccountDomain GetAccount()
        {
            return new AccountDomain();
        }

    }
}