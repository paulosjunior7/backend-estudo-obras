using GraphQL.Types;
using Obras.Business.ResponsibilityDomain.Enums;

namespace Obras.GraphQLModels.ResponsibilityDomain.Enums
{
    public class ResponsibilitySortingFieldsEnumType : EnumerationGraphType<ResponsibilitySortingFields>
    {
        public ResponsibilitySortingFieldsEnumType()
        {
            Name = nameof(ResponsibilitySortingFieldsEnumType);
        }
    }
}
