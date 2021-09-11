namespace Obras.GraphQLModels.Enums
{
    using GraphQL.Types;
    using Obras.Business.Enums;

    public class ProductSortingFieldsEnumType : EnumerationGraphType<ProductSortingFields>
    {
        public ProductSortingFieldsEnumType()
        {
            Name = nameof(ProductSortingFieldsEnumType);
        }
    }
}
