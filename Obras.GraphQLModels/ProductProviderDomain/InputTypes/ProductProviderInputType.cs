using GraphQL.Types;
using Obras.Business.ProductProviderDomain.Models;

namespace Obras.GraphQLModels.ProductProviderDomain.InputTypes
{
    public class ProductProviderInputType : InputObjectGraphType<ProductProviderModel>
    {
        public ProductProviderInputType()
        {
            Name = nameof(ProductProviderInputType);

            Field(x => x.AuxiliaryCode);
            Field(x => x.ProductId);
            Field(x => x.ProviderId);
            Field(x => x.Active);
        }
    }
}
