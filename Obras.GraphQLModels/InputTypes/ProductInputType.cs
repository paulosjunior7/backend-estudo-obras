namespace Obras.GraphQLModels.InputTypes
{
    using GraphQL.Types;
    using Obras.Business.Models;

    public class ProductInputType : InputObjectGraphType<ProductModel>
    {
        public ProductInputType()
        {
            Name = nameof(ProductInputType);

            Field(x => x.Detail);
            Field(x => x.Description, nullable: true);
            Field(x => x.Active);
        }
    }
}
