using HotChocolate;
using HotChocolate.Types;
using ObrasApi.src.Account.Database.Domain;
using ObrasApi.src.Company.Database.Domain;
using ObrasApi.src.Shared.Database;
using System.Linq;

namespace ObrasApi.src.Shared.GraphQL.Account
{
    public class AccountType : ObjectType<AccountDomain>
    {
        protected override void Configure(IObjectTypeDescriptor<AccountDomain> descriptor)
        {
            descriptor.Description("Este modelo é usado como Usuário");
            descriptor.Field(x => x.Company)
                .ResolveWith<Resolvers>(x => x.GetCompany(default!, default!))
                .UseDbContext<AppDbContext>()
                .Description("Esta é a empresa do Usuário");

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