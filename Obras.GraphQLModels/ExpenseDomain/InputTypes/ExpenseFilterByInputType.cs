using GraphQL.Types;
using Obras.Business.ExpenseDomain.Models;
using Obras.GraphQLModels.ExpenseDomain.Enums;

namespace Obras.GraphQLModels.ExpenseDomain.InputTypes
{
    public class ExpenseFilterByInputType : InputObjectGraphType<ExpenseFilter>
    {
        public ExpenseFilterByInputType()
        {
            Field(x => x.Id, nullable: true);
            Field(x => x.Description, nullable: true);
            Field(x => x.CompanyId, nullable: true);
            Field(x => x.Active, nullable: true);

            Field<TypeExpenseEnumType>("typeExpense");
        }
    }
}
