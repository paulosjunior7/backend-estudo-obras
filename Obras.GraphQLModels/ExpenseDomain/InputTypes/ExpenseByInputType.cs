using GraphQL.Types;
using Obras.Business.ExpenseDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.GraphQLModels.ExpenseDomain.Enums;
using Obras.GraphQLModels.SharedDomain.Enums;

namespace Obras.GraphQLModels.ExpenseDomain.InputTypes
{
    public class ExpenseByInputType : InputObjectGraphType<SortingDetails<ExpenseSortingFields>>
    {
        public ExpenseByInputType()
        {
            Field<ExpenseSortingFieldsEnumType>("field", resolve: context => context.Source.Field);
            Field<SortingDirectionEnumType>("direction", resolve: context => context.Source.Direction);
        }
    }
}
