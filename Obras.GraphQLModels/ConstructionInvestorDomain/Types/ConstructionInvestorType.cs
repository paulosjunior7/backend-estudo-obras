using GraphQL.Types;
using Obras.Data;
using Obras.Data.Entities;
using Obras.GraphQLModels.ConstructionDomain.Types;
using Obras.GraphQLModels.PeopleDomain.Types;
using Obras.GraphQLModels.SharedDomain.Types;

namespace Obras.GraphQLModels.ConstructionInvestorDomain.Types
{
    public class ConstructionInvestorType : ObjectGraphType<ConstructionInvestor>
    {
        public ConstructionInvestorType(ObrasDBContext dbContext)
        {
            Name = nameof(ConstructionInvestorType);

            Field(x => x.Id);
            Field(x => x.PeopleId, nullable: true);
            Field(x => x.ConstructionId, nullable: true);
            Field(x => x.Active);

            FieldAsync<UserType>(
                name: "changeUser",
                resolve: async context => await dbContext.User.FindAsync(context.Source.ChangeUserId));

            FieldAsync<UserType>(
                name: "registrationUser",
                resolve: async context => await dbContext.User.FindAsync(context.Source.RegistrationUserId));

            FieldAsync<PeopleType>(
                name: "people",
                resolve: async context => await dbContext.Peoples.FindAsync(context.Source.PeopleId));

            FieldAsync<ConstructionType>(
                name: "construction",
                resolve: async context => await dbContext.Constructions.FindAsync(context.Source.ConstructionId));
        }
    }
}
