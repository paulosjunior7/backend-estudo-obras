using GraphQL.Types;
using Obras.Data;
using Obras.Data.Entities;

namespace Obras.GraphQLModels.Types
{
    public class ResponsibilityType : ObjectGraphType<Responsibility>
    {
        public ResponsibilityType(ObrasDBContext dbContext)
        {
            Name = nameof(ResponsibilityType);

            Field(x => x.Id);
            Field(x => x.Description, nullable: true);
            Field(x => x.CreationDate, nullable: true);
            Field(x => x.ChangeDate, nullable: true);
            Field(x => x.Active);

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
