using GraphQL.Types;
using Obras.Business.ResponsibilityDomain.Models;

namespace Obras.GraphQLModels.ResponsibilityDomain.InputTypes
{
    public class ResponsibilityInputType : InputObjectGraphType<ResponsibilityModel>
    {
        public ResponsibilityInputType()
        {
            Name = nameof(ResponsibilityInputType);

            Field(x => x.Description, nullable: true);
            Field(x => x.Active);
        }
    }
}
