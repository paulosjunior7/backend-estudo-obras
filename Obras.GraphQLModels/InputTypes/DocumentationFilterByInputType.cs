using GraphQL.Types;
using Obras.Business.Models;

namespace Obras.GraphQLModels.InputTypes
{
    public class DocumentationFilterByInputType : InputObjectGraphType<DocumentationFilter>
    {
        public DocumentationFilterByInputType()
        {
            Field(x => x.Id, nullable: true);
            Field(x => x.Description, nullable: true);
            Field(x => x.CompanyId, nullable: true);
            Field(x => x.Active, nullable: true);
        }
    }
}
