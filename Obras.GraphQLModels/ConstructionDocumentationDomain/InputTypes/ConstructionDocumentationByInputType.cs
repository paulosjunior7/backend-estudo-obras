using GraphQL.Types;
using Obras.Business.ConstructionDocumentationDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.GraphQLModels.ConstructionDocumentationDomain.Enums;
using Obras.GraphQLModels.SharedDomain.Enums;

namespace Obras.GraphQLModels.ConstructionDocumentationDomain.InputTypes
{
    public class ConstructionDocumentationByInputType : InputObjectGraphType<SortingDetails<ConstructionDocumentationSortingFields>>
    {
        public ConstructionDocumentationByInputType()
        {
            Field<ConstructionDocumentationSortingFieldsEnumType>("field", resolve: context => context.Source.Field);
            Field<SortingDirectionEnumType>("direction", resolve: context => context.Source.Direction);
        }
    }
}
