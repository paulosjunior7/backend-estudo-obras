using GraphQL.Types;
using Obras.Business.Models;

namespace Obras.GraphQLModels.InputTypes
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
