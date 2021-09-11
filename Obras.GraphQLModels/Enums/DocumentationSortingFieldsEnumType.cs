using GraphQL.Types;
using Obras.Business.Enums;

namespace Obras.GraphQLModels.Enums
{
    public class DocumentationSortingFieldsEnumType : EnumerationGraphType<DocumentationSortingFields>
    {
        public DocumentationSortingFieldsEnumType()
        {
            Name = nameof(DocumentationSortingFieldsEnumType);
        }
    }
}
