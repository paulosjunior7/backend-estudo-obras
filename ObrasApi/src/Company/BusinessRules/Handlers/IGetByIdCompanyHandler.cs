using ObrasApi.src.Company.BusinessRules.Requests;
using ObrasApi.src.Company.BusinessRules.Response;

namespace ObrasApi.src.Company.BusinessRules.Handlers
{
    public interface IGetByIdCompanyHandler
    {
        CompanyResponse Execute(GetByIdCompanyRequest request);
    }
}