using GraphQL.Types;
using Obras.Business.DocumentationDomain.Models;

namespace Obras.GraphQLModels.DocumentationDomain.InputTypes
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
