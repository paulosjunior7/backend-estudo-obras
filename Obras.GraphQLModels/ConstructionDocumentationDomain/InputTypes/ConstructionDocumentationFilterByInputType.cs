using GraphQL.Types;
using Obras.Business.ConstructionDocumentationDomain.Models;

namespace Obras.GraphQLModels.ConstructionDocumentationDomain.InputTypes
{
    public class ConstructionDocumentationFilterByInputType : InputObjectGraphType<ConstructionDocumentationFilter>
    {
        public ConstructionDocumentationFilterByInputType()
        {
            Field(x => x.Id, nullable: true);
            Field(x => x.ConstructionId, nullable: true);
        }
    }
}
