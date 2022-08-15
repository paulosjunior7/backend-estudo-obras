using GraphQL.Types;
using Obras.Business.UnitDomain.Models;

namespace Obras.GraphQLModels.UnityDomain.InputTypes
{
    public class UnityInputType : InputObjectGraphType<UnityModel>
    {
        public UnityInputType()
        {
            Name = nameof(UnityInputType);

            Field(x => x.Description, nullable: true);
            Field(x => x.Multiplier, nullable: true);
            Field(x => x.Active);
        }
    }
}
