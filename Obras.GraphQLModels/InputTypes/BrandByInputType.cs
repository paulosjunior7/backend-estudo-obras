using GraphQL.Types;
using Obras.Business.Enums;
using Obras.Business.Models;
using Obras.GraphQLModels.Enums;

namespace Obras.GraphQLModels.InputTypes
{
    public class BrandByInputType : InputObjectGraphType<SortingDetails<BrandSortingFields>>
    {
        public BrandByInputType()
        {
            Field<BrandSortingFieldsEnumType>("field", resolve: context => context.Source.Field);
            Field<SortingDirectionEnumType>("direction", resolve: context => context.Source.Direction);
        }
    }
}
