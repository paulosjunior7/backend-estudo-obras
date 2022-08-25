using GraphQL.Types;
using Obras.Business.ConstructionDocumentationDomain.Models;

namespace Obras.GraphQLModels.ConstructionDocumentationDomain.InputTypes
{
    public class ConstructionDocumentationInputType : InputObjectGraphType<ConstructionDocumentationModel>
    {
        public ConstructionDocumentationInputType()
        {
            Name = nameof(ConstructionDocumentationInputType);

            Field(x => x.ConstructionInvestorId);
            Field(x => x.ConstructionId);
            Field(x => x.Date);
            Field(x => x.DocumentationId);
            Field(x => x.Value);
            Field(x => x.Active);
        }
    }
}
