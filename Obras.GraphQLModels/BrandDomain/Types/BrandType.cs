using GraphQL.Types;
using Obras.Data;
using Obras.Data.Entities;
using Obras.GraphQLModels.SharedDomain.Types;

namespace Obras.GraphQLModels.BrandDomain.Types
{
    public class BrandType : ObjectGraphType<Brand>
    {
        public BrandType(ObrasDBContext dbContext)
        {
            Name = nameof(BrandType);

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

        }
    }
}
