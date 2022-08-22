using GraphQL.Types;
using Obras.Business.ConstructionMaterialDomain.Models;

namespace Obras.GraphQLModels.ConstructionMaterialDomain.InputTypes
{
    public class ConstructionMaterialFilterByInputType : InputObjectGraphType<ConstructionMaterialFilter>
    {
        public ConstructionMaterialFilterByInputType()
        {
            Field(x => x.GroupId, nullable: true);
            Field(x => x.ConstructionId, nullable: true);
            Field(x => x.ProductId, nullable: true);
            Field(x => x.PurchaseDate, nullable: true);
            Field(x => x.Quantity, nullable: true);
            Field(x => x.UnitPrice, nullable: true);
        }
    }
}
