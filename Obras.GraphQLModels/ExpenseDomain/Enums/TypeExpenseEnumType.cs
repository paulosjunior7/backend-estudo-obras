using GraphQL.Types;
using Obras.Data.Enums;

namespace Obras.GraphQLModels.ExpenseDomain.Enums
{
    public class TypeExpenseEnumType : EnumerationGraphType<TypeExpense>
    {
        public TypeExpenseEnumType()
        {
            Name = nameof(TypeExpenseEnumType);
        }
    }
}
