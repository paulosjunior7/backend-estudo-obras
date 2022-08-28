using GraphQL.Types;
using Obras.Business.ConstructionAdvanceMoneyDomain.Enums;

namespace Obras.GraphQLModels.ConstructionAdvanceMoneyDomain.Enums
{
    public class ConstructionAdvanceMoneySortingFieldsEnumType : EnumerationGraphType<ConstructionAdvanceMoneySortingFields>
    {
        public ConstructionAdvanceMoneySortingFieldsEnumType()
        {
            Name = nameof(ConstructionAdvanceMoneySortingFieldsEnumType);
        }
    }
}
