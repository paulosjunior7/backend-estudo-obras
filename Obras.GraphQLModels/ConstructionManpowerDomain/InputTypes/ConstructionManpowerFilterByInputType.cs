using GraphQL.Types;
using Obras.Business.ConstructionManpowerDomain.Models;

namespace Obras.GraphQLModels.ConstructionManpowerDomain.InputTypes
{
    public class ConstructionManpowerFilterByInputType : InputObjectGraphType<ConstructionManpowerFilter>
    {
        public ConstructionManpowerFilterByInputType()
        {
            Field(x => x.Id, nullable: true);
            Field(x => x.ConstructionId, nullable: true);
        }
    }
}
