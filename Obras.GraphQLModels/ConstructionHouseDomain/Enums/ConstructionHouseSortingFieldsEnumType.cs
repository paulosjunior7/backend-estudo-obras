using GraphQL.Types;
using Obras.Business.ConstructionHouseDomain.Enums;

namespace Obras.GraphQLModels.ConstructionHouseDomain.Enums
{
    public class ConstructionHouseSortingFieldsEnumType : EnumerationGraphType<ConstructionHouseSortingFields>
    {
        public ConstructionHouseSortingFieldsEnumType()
        {
            Name = nameof(ConstructionHouseSortingFieldsEnumType);
        }
    }
}
