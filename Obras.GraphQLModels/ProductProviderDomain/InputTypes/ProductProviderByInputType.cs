using GraphQL.Types;
using Obras.Business.ProductProviderDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.GraphQLModels.ProductProviderDomain.Enums;
using Obras.GraphQLModels.SharedDomain.Enums;

namespace Obras.GraphQLModels.ProductProviderDomain.InputTypes
{
    public class ProductProviderByInputType : InputObjectGraphType<SortingDetails<ProductProviderSortingFields>>
    {
        public ProductProviderByInputType()
        {
            Field<ProductProviderSortingFieldsEnumType>("field", resolve: context => context.Source.Field);
            Field<SortingDirectionEnumType>("direction", resolve: context => context.Source.Direction);
        }
    }
}
