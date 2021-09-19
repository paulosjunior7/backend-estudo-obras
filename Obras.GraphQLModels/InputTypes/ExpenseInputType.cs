using GraphQL.Types;
using Obras.Business.Models;
using Obras.GraphQLModels.Enums;

namespace Obras.GraphQLModels.InputTypes
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
