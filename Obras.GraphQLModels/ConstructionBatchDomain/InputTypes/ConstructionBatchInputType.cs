using GraphQL.Types;
using Obras.Business.ConstructionBatchDomain.Models;

namespace Obras.GraphQLModels.ConstructionBatchDomain.InputTypes
{
    public class ConstructionBatchInputType : InputObjectGraphType<ConstructionBatchModel>
    {
        public ConstructionBatchInputType()
        {
            Name = nameof(ConstructionBatchInputType);

            Field(x => x.PeopleId);
            Field(x => x.ConstructionId);
            Field(x => x.Active);
            Field(x => x.Value);
        }
    }
}
