namespace Obras.GraphQLModels.ProductDomain.InputTypes
{
    using GraphQL.Types;
    using Obras.Business.ProductDomain.Enums;
    using Obras.Business.SharedDomain.Models;
    using Obras.GraphQLModels.ProductDomain.Enums;
    using Obras.GraphQLModels.SharedDomain.Enums;

    public class ProductByInputType : InputObjectGraphType<SortingDetails<ProductSortingFields>>
    {
        public ProductByInputType()
        {
            Field<ProductSortingFieldsEnumType>("field", resolve: context => context.Source.Field);
            Field<SortingDirectionEnumType>("direction", resolve: context => context.Source.Direction);
        }
    }
}
