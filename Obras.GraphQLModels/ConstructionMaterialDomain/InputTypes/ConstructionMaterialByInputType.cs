using GraphQL.Types;
using Obras.Business.ConstructionMaterialDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.GraphQLModels.ConstructionMaterialDomain.Enums;
using Obras.GraphQLModels.SharedDomain.Enums;

namespace Obras.GraphQLModels.ConstructionMaterialDomain.InputTypes
{
    public class ConstructionMaterialByInputType : InputObjectGraphType<SortingDetails<ConstructionMaterialSortingFields>>
    {
        public ConstructionMaterialByInputType()
        {
            Field<ConstructionMaterialSortingFieldsEnumType>("field", resolve: context => context.Source.Field);
            Field<SortingDirectionEnumType>("direction", resolve: context => context.Source.Direction);
        }
    }
}
