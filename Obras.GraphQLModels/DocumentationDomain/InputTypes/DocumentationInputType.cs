using GraphQL.Types;
using Obras.Business.DocumentationDomain.Models;

namespace Obras.GraphQLModels.DocumentationDomain.InputTypes
{
    public class DocumentationInputType : InputObjectGraphType<DocumentationModel>
    {
        public DocumentationInputType()
        {
            Name = nameof(DocumentationInputType);

            Field(x => x.Description, nullable: true);
            Field(x => x.Active);
        }
    }
}
