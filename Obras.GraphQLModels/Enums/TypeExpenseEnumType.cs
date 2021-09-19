using GraphQL.Types;
using Obras.Data.Enums;

namespace Obras.GraphQLModels.Enums
{
    public class TypeExpenseEnumType : EnumerationGraphType<TypeExpense>
    {
        public TypeExpenseEnumType()
        {
            Name = nameof(TypeExpenseEnumType);
        }
    }
}
