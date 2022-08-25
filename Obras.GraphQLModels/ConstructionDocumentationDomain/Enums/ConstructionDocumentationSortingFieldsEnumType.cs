using GraphQL.Types;
using Obras.Business.ConstructionDocumentationDomain.Enums;

namespace Obras.GraphQLModels.ConstructionDocumentationDomain.Enums
{
    public class ConstructionDocumentationSortingFieldsEnumType : EnumerationGraphType<ConstructionDocumentationSortingFields>
    {
        public ConstructionDocumentationSortingFieldsEnumType()
        {
            Name = nameof(ConstructionDocumentationSortingFieldsEnumType);
        }
    }
}
