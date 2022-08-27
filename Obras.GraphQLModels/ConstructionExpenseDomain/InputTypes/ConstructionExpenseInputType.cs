using GraphQL.Types;
using Obras.Business.ConstructionExpenseDomain.Models;

namespace Obras.GraphQLModels.ConstructionExpenseDomain.InputTypes
{
    public class ConstructionExpenseInputType : InputObjectGraphType<ConstructionExpenseModel>
    {
        public ConstructionExpenseInputType()
        {
            Name = nameof(ConstructionExpenseInputType);

            Field(x => x.ConstructionInvestorId);
            Field(x => x.ConstructionId);
            Field(x => x.Date);
            Field(x => x.ExpenseId);
            Field(x => x.Value);
            Field(x => x.Active);
        }
    }
}
