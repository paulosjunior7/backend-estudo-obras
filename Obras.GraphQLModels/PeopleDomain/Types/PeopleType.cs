using GraphQL.Types;
using Obras.Data;
using Obras.Data.Entities;
using Obras.GraphQLModels.CompanyDomain.Types;
using Obras.GraphQLModels.SharedDomain.Types;

namespace Obras.GraphQLModels.PeopleDomain.Types
{
    public class PeopleType : ObjectGraphType<People>
    {
        public PeopleType(ObrasDBContext dbContext)
        {
            Name = nameof(PeopleType);

            Field(x => x.Id);
            Field(x => x.Neighbourhood, nullable: true);
            Field(x => x.Number, nullable: true);
            Field(x => x.State, nullable: true);
            Field(x => x.Telephone, nullable: true);
            Field(x => x.ZipCode, nullable: true);
            Field(x => x.Active);
            Field(x => x.Address, nullable: true);
            Field(x => x.CellPhone, nullable: true);
            Field(x => x.ChangeDate, nullable: true);
            Field(x => x.City, nullable: true);
            Field(x => x.Cnpj, nullable: true);
            Field(x => x.Cpf, nullable: true);
            Field(x => x.Complement, nullable: true);
            Field(x => x.CorporateName, nullable: true);
            Field(x => x.CreationDate, nullable: true);
            Field(x => x.EMail, nullable: true);
            Field(x => x.FantasyName, nullable: true);

            Field<StringGraphType>(
                name: "typePeople",
                resolve: context => context.Source.TypePeople.ToString());

            FieldAsync<UserType>(
                name: "changeUser",
                resolve: async context => await dbContext.User.FindAsync(context.Source.ChangeUserId));

            FieldAsync<UserType>(
                name: "registrationUser",
                resolve: async context => await dbContext.User.FindAsync(context.Source.RegistrationUserId));

            FieldAsync<CompanyType>(
                name: "company",
                resolve: async context => await dbContext.Companies.FindAsync(context.Source.CompanyId));
        }
    }
}
