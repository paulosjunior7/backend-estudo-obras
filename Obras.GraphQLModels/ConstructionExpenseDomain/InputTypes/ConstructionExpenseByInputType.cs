using GraphQL.Types;
using Obras.Business.ConstructionExpenseDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.GraphQLModels.ConstructionExpenseDomain.Enums;
using Obras.GraphQLModels.SharedDomain.Enums;

namespace Obras.GraphQLModels.ConstructionExpenseDomain.InputTypes
{
    public class ConstructionExpenseByInputType : InputObjectGraphType<SortingDetails<ConstructionExpenseSortingFields>>
    {
        public ConstructionExpenseByInputType()
        {
            Field<ConstructionExpenseSortingFieldsEnumType>("field", resolve: context => context.Source.Field);
            Field<SortingDirectionEnumType>("direction", resolve: context => context.Source.Direction);
        }
    }
}
