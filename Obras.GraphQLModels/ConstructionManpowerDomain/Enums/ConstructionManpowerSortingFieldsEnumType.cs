using GraphQL.Types;
using Obras.Business.ConstructionManpowerDomain.Enums;

namespace Obras.GraphQLModels.ConstructionManpowerDomain.Enums
{
    public class ConstructionManpowerSortingFieldsEnumType : EnumerationGraphType<ConstructionManpowerSortingFields>
    {
        public ConstructionManpowerSortingFieldsEnumType()
        {
            Name = nameof(ConstructionManpowerSortingFieldsEnumType);
        }
    }
}
