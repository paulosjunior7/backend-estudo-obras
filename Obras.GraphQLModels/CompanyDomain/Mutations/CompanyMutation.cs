namespace Obras.GraphQLModels.CompanyDomain.Mutations
{
    using HotChocolate;

    using Obras.Business.CompanyDomain.Models;
    using Obras.Business.CompanyDomain.Services;
    using Obras.Data.Entities;
    using System.Threading.Tasks;


    public class CompanyMutation
    {
        public async Task<Company> CreateCompany([Service] ICompanyService companyService, CompanyModel company) =>
            await companyService.CreateAsync(company);

        public async Task<Company> UpdateCompany([Service] ICompanyService companyService, int id, CompanyModel company) =>
            await companyService.UpdateCompanyAsync(id, company);
    }
}
