using ObrasApi.src.Account.Database.Domain;

namespace ObrasApi.src.Shared.GraphQL.Account
{
    public record AddAccountPayload(AccountDomain account);
}