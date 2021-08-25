using ObrasApi.src.Company.Database.Domain;

namespace ObrasApi.src.Shared.GraphQL.Company
{
    public record AddCompanyPayload(CompanyDomain company);
}