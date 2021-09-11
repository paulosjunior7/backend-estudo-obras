using GraphQL.Types;
using Obras.Business.Models;

namespace Obras.GraphQLModels.InputTypes
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
