using GraphQL.Types;
using Obras.Business.Enums;

namespace Obras.GraphQLModels.Enums
{
    public class ResponsibilitySortingFieldsEnumType : EnumerationGraphType<ResponsibilitySortingFields>
    {
        public ResponsibilitySortingFieldsEnumType()
        {
            Name = nameof(ResponsibilitySortingFieldsEnumType);
        }
    }
}
