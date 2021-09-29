using GraphQL.Types;
using Obras.Business.BrandDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.GraphQLModels.BrandDomain.Enums;
using Obras.GraphQLModels.SharedDomain.Enums;

namespace Obras.GraphQLModels.BrandDomain.InputTypes
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
