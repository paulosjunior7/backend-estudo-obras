using HotChocolate;
using HotChocolate.Types;
using ObrasApi.src.Account.Database.Domain;
using ObrasApi.src.Company.Database.Domain;
using ObrasApi.src.Shared.Database;
using System.Linq;

namespace ObrasApi.src.Shared.GraphQL.Company
{
    public class CompanyType : ObjectType<CompanyDomain>
    {
        protected override void Configure(IObjectTypeDescriptor<CompanyDomain> descriptor)
        {
            descriptor.Description("Este modelo é usado como Empresa");
            descriptor.Field(x => x.Id)
                .Description("Este é o Codigo único da Empresa");
            descriptor.Field(x => x.FantasyName)
                .Description("Este é o Nome Fantasia da empresa");

        }

        private class Resolvers
        {
            public IQueryable<CompanyDomain> GetCompany(AccountDomain account, [ScopedService] AppDbContext context)
            {
                return context.Companies.Where(x => x.Id == account.IdCompany);
            }
        }
    }
}