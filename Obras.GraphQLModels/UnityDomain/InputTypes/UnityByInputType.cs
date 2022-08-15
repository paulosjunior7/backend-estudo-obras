using GraphQL.Types;
using Obras.Business.SharedDomain.Models;
using Obras.Business.UnitDomain.Enums;
using Obras.GraphQLModels.SharedDomain.Enums;
using Obras.GraphQLModels.UnityDomain.Enums;

namespace Obras.GraphQLModels.UnityDomain.InputTypes
{
    public class UnityByInputType : InputObjectGraphType<SortingDetails<UnitySortingFields>>
    {
        public UnityByInputType()
        {
            Field<UnitySortingFieldsEnumType>("field", resolve: context => context.Source.Field);
            Field<SortingDirectionEnumType>("direction", resolve: context => context.Source.Direction);
        }
    }
}
