using GraphQL.Types;
using Obras.Business.UnitDomain.Enums;

namespace Obras.GraphQLModels.UnityDomain.Enums
{
    public class UnitySortingFieldsEnumType : EnumerationGraphType<UnitySortingFields>
    {
        public UnitySortingFieldsEnumType()
        {
            Name = nameof(UnitySortingFieldsEnumType);
        }
    }
}
