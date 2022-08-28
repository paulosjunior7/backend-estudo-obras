using GraphQL.Types;
using Obras.Business.ConstructionAdvanceMoneyDomain.Models;

namespace Obras.GraphQLModels.ConstructionAdvanceMoneyDomain.InputTypes
{
    public class ConstructionAdvanceMoneyFilterByInputType : InputObjectGraphType<ConstructionAdvanceMoneyFilter>
    {
        public ConstructionAdvanceMoneyFilterByInputType()
        {
            Field(x => x.Id, nullable: true);
            Field(x => x.ConstructionId, nullable: true);
        }
    }
}
