using HotChocolate;
using HotChocolate.Data;
using ObrasApi.src.Account.Database.Domain;
using ObrasApi.src.Company.Database.Domain;
using ObrasApi.src.Shared.Database;
using System.Linq;

namespace ObrasApi.src.Shared.GraphQL
{
    public class Query
    {
        [UseDbContext(typeof(AppDbContext))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<CompanyDomain> GetCompanies([ScopedService] AppDbContext context)
        {
            return context.Companies;
        }

        [UseDbContext(typeof(AppDbContext))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<AccountDomain> GetAccounts([ScopedService] AppDbContext context)
        {
            return context.Accounts;
        }
    }
}