using GraphQL.Types;
using Obras.Business.ExpenseDomain.Models;
using Obras.GraphQLModels.ExpenseDomain.Enums;

namespace Obras.GraphQLModels.ExpenseDomain.InputTypes
{
    public class ExpenseInputType : InputObjectGraphType<ExpenseModel>
    {
        public ExpenseInputType()
        {
            Name = nameof(ExpenseInputType);

            Field(x => x.Description, nullable: true);
            Field(x => x.Active);
            Field<TypeExpenseEnumType>("typeExpense");
        }
    }
}
