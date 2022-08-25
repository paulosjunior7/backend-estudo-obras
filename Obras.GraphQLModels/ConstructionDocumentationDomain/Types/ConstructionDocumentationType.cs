using GraphQL.Types;
using Obras.Data;
using Obras.Data.Entities;
using Obras.GraphQLModels.ConstructionDomain.Types;
using Obras.GraphQLModels.ConstructionInvestorDomain.Types;
using Obras.GraphQLModels.DocumentationDomain.Types;
using Obras.GraphQLModels.SharedDomain.Types;

namespace Obras.GraphQLModels.ConstructionDocumentationDomain.Types
{
    public class ConstructionDocumentationType : ObjectGraphType<ConstructionDocumentation>
    {
        public ConstructionDocumentationType(ObrasDBContext dbContext)
        {
            Name = nameof(ConstructionDocumentationType);

            Field(x => x.Id);
            Field(x => x.DocumentationId);
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

            FieldAsync<DocumentationType>(
                name: "documentation",
                resolve: async context => await dbContext.Documentations.FindAsync(context.Source.DocumentationId));

            FieldAsync<ConstructionType>(
                name: "construction",
                resolve: async context => await dbContext.Constructions.FindAsync(context.Source.ConstructionId));
        }
    }
}
