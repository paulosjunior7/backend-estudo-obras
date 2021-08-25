using HotChocolate;
using ObrasApi.src.Company.BusinessRules.Handlers;
using ObrasApi.src.Company.BusinessRules.Requests;
using ObrasApi.src.Company.BusinessRules.Response;

namespace ObrasApi.src.Company.Api
{
    public class CompanyQuery
    {
        public CompaniesResponse GetCompanies([Service] IGetAllCompaniesHandler handler)
        {
            return handler.Execute();
        }

        public CompanyResponse GetCompany([Service] IGetByIdCompanyHandler handler, GetByIdCompanyRequest request)
        {
            return handler.Execute(request);
        }
    }
}