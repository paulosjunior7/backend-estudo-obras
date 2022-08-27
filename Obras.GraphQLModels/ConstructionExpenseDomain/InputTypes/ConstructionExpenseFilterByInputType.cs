using GraphQL.Types;
using Obras.Business.ConstructionExpenseDomain.Models;

namespace Obras.GraphQLModels.ConstructionExpenseDomain.InputTypes
{
    public class ConstructionExpenseFilterByInputType : InputObjectGraphType<ConstructionExpenseFilter>
    {
        public ConstructionExpenseFilterByInputType()
        {
            Field(x => x.Id, nullable: true);
            Field(x => x.ConstructionId, nullable: true);
        }
    }
}
