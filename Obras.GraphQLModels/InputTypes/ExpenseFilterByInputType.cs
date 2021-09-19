using GraphQL.Types;
using Obras.Business.Models;
using Obras.GraphQLModels.Enums;

namespace Obras.GraphQLModels.InputTypes
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
