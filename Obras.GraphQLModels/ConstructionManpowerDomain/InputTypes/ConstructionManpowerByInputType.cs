using GraphQL.Types;
using Obras.Business.ConstructionManpowerDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.GraphQLModels.ConstructionManpowerDomain.Enums;
using Obras.GraphQLModels.SharedDomain.Enums;

namespace Obras.GraphQLModels.ConstructionManpowerDomain.InputTypes
{ 
    public class ConstructionManpowerByInputType : InputObjectGraphType<SortingDetails<ConstructionManpowerSortingFields>>
    {
        public ConstructionManpowerByInputType()
        {
            Field<ConstructionManpowerSortingFieldsEnumType>("field", resolve: context => context.Source.Field);
            Field<SortingDirectionEnumType>("direction", resolve: context => context.Source.Direction);
        }
    }
}
