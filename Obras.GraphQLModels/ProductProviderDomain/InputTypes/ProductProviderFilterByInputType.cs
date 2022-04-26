using GraphQL.Types;
using Obras.Business.ProductProviderDomain.Models;

namespace Obras.GraphQLModels.ProductProviderDomain.InputTypes
{
    public class ProductProviderFilterByInputType : InputObjectGraphType<ProductProviderFilter>
    {
        public ProductProviderFilterByInputType()
        {
            Field(x => x.Id, nullable: true);
            Field(x => x.AuxiliaryCode, nullable: true);
            Field(x => x.ProductId, nullable: true);
            Field(x => x.ProviderId, nullable: true);
            Field(x => x.Active, nullable: true);
        }
    }
}
