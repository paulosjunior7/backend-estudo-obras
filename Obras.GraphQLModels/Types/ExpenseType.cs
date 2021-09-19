using GraphQL.Types;
using Obras.Data;
using Obras.Data.Entities;

namespace Obras.GraphQLModels.Types
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

            FieldAsync<CompanyType>(
                name: "company",
                resolve: async context => await dbContext.Companies.FindAsync(context.Source.CompanyId));
        }
    }
}
