using HotChocolate;
using ObrasApi.src.Account.BusinessRules.Handlers;
using ObrasApi.src.Account.BusinessRules.Requests;
using ObrasApi.src.Account.BusinessRules.Response;

namespace ObrasApi.src.Account.Api
{
    public class AccountMutation
    {
        public UpsertAccountResponse UpsertAccount([Service] IUpsertAccountHandler handler, UpsertAccountRequest request)
        {
            return handler.Execute(request);
        }
    }
}