using GraphQL.Types;
using Obras.Business.DocumentationDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.GraphQLModels.DocumentationDomain.Enums;
using Obras.GraphQLModels.SharedDomain.Enums;

namespace Obras.GraphQLModels.DocumentationDomain.InputTypes
{
    public class DocumentationByInputType : InputObjectGraphType<SortingDetails<DocumentationSortingFields>>
    {
        public DocumentationByInputType()
        {
            Field<DocumentationSortingFieldsEnumType>("field", resolve: context => context.Source.Field);
            Field<SortingDirectionEnumType>("direction", resolve: context => context.Source.Direction);
        }
    }
}
