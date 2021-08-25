using ObrasApi.src.Company.BusinessRules.Response;

namespace ObrasApi.src.Company.BusinessRules.Handlers
{
    public interface IGetAllCompaniesHandler
    {
        CompaniesResponse Execute();
    }
}