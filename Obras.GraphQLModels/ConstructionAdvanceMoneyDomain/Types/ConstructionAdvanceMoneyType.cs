using GraphQL.Types;
using Obras.Data;
using Obras.Data.Entities;
using Obras.GraphQLModels.ConstructionDomain.Types;
using Obras.GraphQLModels.ConstructionInvestorDomain.Types;
using Obras.GraphQLModels.SharedDomain.Types;

namespace Obras.GraphQLModels.ConstructionAdvanceMoneyDomain.Types
{
    public class ConstructionAdvanceMoneyType : ObjectGraphType<ConstructionAdvanceMoney>
    {
        public ConstructionAdvanceMoneyType(ObrasDBContext dbContext)
        {
            Name = nameof(ConstructionAdvanceMoneyType);

            Field(x => x.Id);
            Field(x => x.Value);
            Field(x => x.Date);
            Field(x => x.ConstructionInvestorId);
            Field(x => x.Active);

            FieldAsync<UserType>(
                name: "changeUser",
                resolve: async context => await dbContext.User.FindAsync(context.Source.ChangeUserId));

            FieldAsync<UserType>(
                name: "registrationUser",
                resolve: async context => await dbContext.User.FindAsync(context.Source.RegistrationUserId));

            FieldAsync<ConstructionInvestorType>(
                name: "constructionInvestor",
                resolve: async context => await dbContext.ConstructionInvestors.FindAsync(context.Source.ConstructionInvestorId));

            FieldAsync<ConstructionType>(
                name: "construction",
                resolve: async context => await dbContext.Constructions.FindAsync(context.Source.ConstructionId));
        }
    }
}
