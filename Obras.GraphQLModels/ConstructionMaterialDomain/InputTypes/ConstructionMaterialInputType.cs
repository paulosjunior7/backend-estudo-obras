using GraphQL.Types;
using Obras.Business.ConstructionMaterialDomain.Models;

namespace Obras.GraphQLModels.ConstructionMaterialDomain.InputTypes
{
    public class ConstructionMaterialInputType : InputObjectGraphType<ConstructionMaterialModel>
    {
        public ConstructionMaterialInputType()
        {
            Name = nameof(ConstructionMaterialInputType);

            Field(x => x.BrandId);
            Field(x => x.ConstructionInvestorId);
            Field(x => x.GroupId);
            Field(x => x.ProductId);
            Field(x => x.ProviderId);
            Field(x => x.Quantity);
            Field(x => x.UnitPrice);
            Field(x => x.UnityId);
            Field(x => x.ConstructionId);
            Field(x => x.Active);
        }
    }
}
