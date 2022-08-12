using GraphQL.Types;
using Obras.Business.ConstructionBatchDomain.Models;
using Obras.Business.SharedDomain.Models;

namespace Obras.GraphQLModels.ConstructionBatchDomain.InputTypes
{
    public class ConstructionBatchFilterByInputType : InputObjectGraphType<ConstructionBatchFilter>
    {
        public ConstructionBatchFilterByInputType()
        {
            Field(x => x.Id, nullable: true);
            Field(x => x.ConstructionId, nullable: true);
            Field(x => x.PeopleId, nullable: true);
        }
    }
}
