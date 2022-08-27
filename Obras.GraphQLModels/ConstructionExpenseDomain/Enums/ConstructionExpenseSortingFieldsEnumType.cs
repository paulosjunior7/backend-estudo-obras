using GraphQL.Types;
using Obras.Business.ConstructionExpenseDomain.Enums;

namespace Obras.GraphQLModels.ConstructionExpenseDomain.Enums
{
    public class ConstructionExpenseSortingFieldsEnumType : EnumerationGraphType<ConstructionExpenseSortingFields>
    {
        public ConstructionExpenseSortingFieldsEnumType()
        {
            Name = nameof(ConstructionExpenseSortingFieldsEnumType);
        }
    }
}
