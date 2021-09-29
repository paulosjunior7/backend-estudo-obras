using GraphQL.Types;
using Obras.Business.DocumentationDomain.Enums;

namespace Obras.GraphQLModels.DocumentationDomain.Enums
{
    public class DocumentationSortingFieldsEnumType : EnumerationGraphType<DocumentationSortingFields>
    {
        public DocumentationSortingFieldsEnumType()
        {
            Name = nameof(DocumentationSortingFieldsEnumType);
        }
    }
}
