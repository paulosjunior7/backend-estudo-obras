using GraphQL.Types;
using Obras.Business.Enums;
using Obras.Business.Models;
using Obras.GraphQLModels.Enums;

namespace Obras.GraphQLModels.InputTypes
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
