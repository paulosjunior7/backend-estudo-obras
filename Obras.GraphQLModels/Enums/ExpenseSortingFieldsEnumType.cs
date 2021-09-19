using GraphQL.Types;
using Obras.Business.Enums;

namespace Obras.GraphQLModels.Enums
{
    public class ExpenseSortingFieldsEnumType : EnumerationGraphType<ExpenseSortingFields>
    {
        public ExpenseSortingFieldsEnumType()
        {
            Name = nameof(ExpenseSortingFieldsEnumType);
        }
    }
}
