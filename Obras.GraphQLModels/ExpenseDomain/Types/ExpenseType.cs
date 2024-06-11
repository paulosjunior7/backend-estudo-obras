using GraphQL.Types;
using Obras.Data;
using Obras.Data.Entities;
using Obras.GraphQLModels.SharedDomain.Types;

namespace Obras.GraphQLModels.ExpenseDomain.Types
{
    public class ExpenseType : ObjectGraphType<Expense>
    {
        public ExpenseType(ObrasDBContext dbContext)
        {
            Name = nameof(ExpenseType);

            Field(x => x.Id);
            Field(x => x.Description, nullable: true);
            Field(x => x.CreationDate, nullable: true);
            Field(x => x.ChangeDate, nullable: true);
            Field(x => x.Active);

            Field<StringGraphType>(
                name: "typeExpense",
                resolve: context => context.Source.TypeExpense.ToString());

            FieldAsync<UserType>(
                name: "changeUser",
                resolve: async context => await dbContext.User.FindAsync(context.Source.ChangeUserId));

            FieldAsync<UserType>(
                name: "registrationUser",
                resolve: async context => await dbContext.User.FindAsync(context.Source.RegistrationUserId));
        }
    }
}
