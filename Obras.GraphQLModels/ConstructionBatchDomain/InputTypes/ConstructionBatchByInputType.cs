using GraphQL.Types;
using Obras.Business.ConstructionBatchDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.GraphQLModels.ConstructionBatchDomain.Enums;
using Obras.GraphQLModels.SharedDomain.Enums;

namespace Obras.GraphQLModels.ConstructionBatchDomain.InputTypes
{
    public class ConstructionBatchByInputType : InputObjectGraphType<SortingDetails<ConstructionBatchSortingFields>>
    {
        public ConstructionBatchByInputType()
        {
            Field<ConstructionBatchSortingFieldsEnumType>("field", resolve: context => context.Source.Field);
            Field<SortingDirectionEnumType>("direction", resolve: context => context.Source.Direction);
        }
    }
}
