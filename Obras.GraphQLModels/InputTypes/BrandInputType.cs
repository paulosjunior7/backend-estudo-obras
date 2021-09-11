using GraphQL.Types;
using Obras.Business.Models;

namespace Obras.GraphQLModels.InputTypes
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
