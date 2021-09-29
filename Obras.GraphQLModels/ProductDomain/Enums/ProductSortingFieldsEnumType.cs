namespace Obras.GraphQLModels.ProductDomain.Enums
{
    using GraphQL.Types;
    using Obras.Business.ProductDomain.Enums;

    public class ProductSortingFieldsEnumType : EnumerationGraphType<ProductSortingFields>
    {
        public ProductSortingFieldsEnumType()
        {
            Name = nameof(ProductSortingFieldsEnumType);
        }
    }
}
