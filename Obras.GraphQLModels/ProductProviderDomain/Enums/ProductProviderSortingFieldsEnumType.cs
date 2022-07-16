using GraphQL.Types;
using Obras.Business.ProductProviderDomain.Enums;

namespace Obras.GraphQLModels.ProductProviderDomain.Enums
{
    public class ProductProviderSortingFieldsEnumType : EnumerationGraphType<ProductProviderSortingFields>
    {
        public ProductProviderSortingFieldsEnumType()
        {
            Name = nameof(ProductProviderSortingFieldsEnumType);
        }
    }
}
