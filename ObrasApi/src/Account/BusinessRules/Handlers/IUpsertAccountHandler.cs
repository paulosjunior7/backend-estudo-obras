using ObrasApi.src.Account.BusinessRules.Requests;
using ObrasApi.src.Account.BusinessRules.Response;

namespace ObrasApi.src.Account.BusinessRules.Handlers
{
    public interface IUpsertAccountHandler
    {
        UpsertAccountResponse Execute(UpsertAccountRequest request);
    }
}