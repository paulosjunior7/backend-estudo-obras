using GraphQL.Types;
using Obras.Business.ExpenseDomain.Enums;

namespace Obras.GraphQLModels.ExpenseDomain.Enums
{
    public class ExpenseSortingFieldsEnumType : EnumerationGraphType<ExpenseSortingFields>
    {
        public ExpenseSortingFieldsEnumType()
        {
            Name = nameof(ExpenseSortingFieldsEnumType);
        }
    }
}
