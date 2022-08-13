using GraphQL.Types;
using Obras.Business.ConstructionHouseDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.GraphQLModels.ConstructionHouseDomain.Enums;
using Obras.GraphQLModels.SharedDomain.Enums;

namespace Obras.GraphQLModels.ConstructionHouseDomain.InputTypes
{
    public class ConstructionHouseByInputType : InputObjectGraphType<SortingDetails<ConstructionHouseSortingFields>>
    {
        public ConstructionHouseByInputType()
        {
            Field<ConstructionHouseSortingFieldsEnumType>("field", resolve: context => context.Source.Field);
            Field<SortingDirectionEnumType>("direction", resolve: context => context.Source.Direction);
        }
    }
}
