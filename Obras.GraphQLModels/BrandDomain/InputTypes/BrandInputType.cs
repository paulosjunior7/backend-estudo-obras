using GraphQL.Types;
using Obras.Business.BrandDomain.Models;

namespace Obras.GraphQLModels.BrandDomain.InputTypes
{
    public class BrandInputType : InputObjectGraphType<BrandModel>
    {
        public BrandInputType()
        {
            Name = nameof(BrandInputType);

            Field(x => x.Description, nullable: true);
            Field(x => x.Active);
        }
    }
}
