using HotChocolate;
using ObrasApi.src.Company.BusinessRules.Handlers;
using ObrasApi.src.Company.BusinessRules.Requests;
using ObrasApi.src.Company.BusinessRules.Response;

namespace ObrasApi.src.Company.Api
{
    public class CompanyMutation
    {
        public UpsertCompanyResponse UpsertCompany([Service] IUpsertCompanyHandler handler, UpsertCompanyRequest request)
        {
            return handler.Execute(request);
        }
    }
}