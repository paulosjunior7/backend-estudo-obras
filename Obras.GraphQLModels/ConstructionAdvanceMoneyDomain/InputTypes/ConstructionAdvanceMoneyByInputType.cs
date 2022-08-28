using GraphQL.Types;
using Obras.Business.ConstructionAdvanceMoneyDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.GraphQLModels.ConstructionAdvanceMoneyDomain.Enums;
using Obras.GraphQLModels.SharedDomain.Enums;

namespace Obras.GraphQLModels.ConstructionAdvanceMoneyDomain.InputTypes
{
    public class ConstructionAdvanceMoneyByInputType : InputObjectGraphType<SortingDetails<ConstructionAdvanceMoneySortingFields>>
    {
        public ConstructionAdvanceMoneyByInputType()
        {
            Field<ConstructionAdvanceMoneySortingFieldsEnumType>("field", resolve: context => context.Source.Field);
            Field<SortingDirectionEnumType>("direction", resolve: context => context.Source.Direction);
        }
    }
}
