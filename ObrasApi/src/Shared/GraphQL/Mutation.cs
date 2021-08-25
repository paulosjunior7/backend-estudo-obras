using HotChocolate;
using HotChocolate.Data;
using ObrasApi.src.Company.Database.Domain;
using ObrasApi.src.Account.Database.Domain;
using ObrasApi.src.Shared.Database;
using ObrasApi.src.Shared.GraphQL.Company;
using System.Threading.Tasks;
using ObrasApi.src.Shared.GraphQL.Account;

namespace ObrasApi.src.Shared.GraphQL
{
    public class Mutation
    {
        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddCompanyPayload> AddCompanyAsync(AddCompanyInput input, [ScopedService] AppDbContext context)
        {
            var company = new CompanyDomain
            {
                Telephone = input.telephone,
                Active = input.active,
                Address = input.address,
                CellPhone = input.cellPhone,
                City = input.city,
                Cnpj = input.cnpj,
                Complement = input.complement,
                CorporateName = input.corporateName,
                EMail = input.eMail,
                FantasyName = input.fantasyName,
                Neighbourhood = input.neighbourhood,
                Number = input.number,
                CreationDate = System.DateTime.Now,
                ChangeDate = System.DateTime.Now
            };

            context.Companies.Add(company);
            await context.SaveChangesAsync();

            return new AddCompanyPayload(company);
        }

        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddAccountPayload> AddAccountAsync(AddAccountInput input, [ScopedService] AppDbContext context)
        {
            var account = new AccountDomain
            {
                Active = input.active,
                EMail = input.eMail,
                IdCompany = input.idCompany,
                Name = input.name,
                Password = input.password,
                CreationDate = System.DateTime.Now,
                ChangeDate = System.DateTime.Now
            };

            context.Accounts.Add(account);
            await context.SaveChangesAsync();

            return new AddAccountPayload(account);
        }
    }
}