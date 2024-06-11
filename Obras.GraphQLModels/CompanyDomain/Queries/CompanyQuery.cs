namespace Obras.GraphQLModels.CompanyDomain.Queries
{
    using HotChocolate;
    using HotChocolate.Data;
    using HotChocolate.Types;
    using Obras.Business.CompanyDomain.Services;
    using Obras.Data.Entities;
    using System.Collections.Generic;

    using System.Threading.Tasks;


    public class CompanyQuery
    {
        [UsePaging(IncludeTotalCount = true)]
        [UseFiltering]
        [UseSorting]
        public async Task<IEnumerable<Company>> CompaniesAsync([Service] ICompanyService companyService) =>
          await companyService.GetCompaniesAsync();

        public async Task<Company> Company([Service] ICompanyService companyService, int id) =>
            await companyService.GetCompanyId(id);
    }
}
