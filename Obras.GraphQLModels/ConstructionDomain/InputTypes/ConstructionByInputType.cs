using GraphQL.Types;
using Obras.Business.ConstructionDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.GraphQLModels.ConstructionDomain.Enums;
using Obras.GraphQLModels.SharedDomain.Enums;

namespace Obras.GraphQLModels.ConstructionDomain.InputTypes
{
    public class ConstructionByInputType : InputObjectGraphType<SortingDetails<ConstructionSortingFields>>
    {
        public ConstructionByInputType()
        {
            Field<ConstructionSortingFieldsEnumType>("field", resolve: context => context.Source.Field);
            Field<SortingDirectionEnumType>("direction", resolve: context => context.Source.Direction);
        }
    }
}
