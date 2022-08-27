namespace Obras.GraphQLModels.ConstructionExpenseDomain.Types
{
    using GraphQL.Types;
    using Obras.Data;
    using Obras.Data.Entities;
    using Obras.GraphQLModels.ConstructionDomain.Types;
    using Obras.GraphQLModels.ConstructionInvestorDomain.Types;
    using Obras.GraphQLModels.ExpenseDomain.Types;
    using Obras.GraphQLModels.SharedDomain.Types;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ConstructionExpenseType : ObjectGraphType<ConstructionExpense>
    {
        public ConstructionExpenseType(ObrasDBContext dbContext)
        {
            Name = nameof(ConstructionExpenseType);

            Field(x => x.Id);
            Field(x => x.ExpenseId);
            Field(x => x.Value);
            Field(x => x.Date);
            Field(x => x.ConstructionInvestorId);
            Field(x => x.Active);

            FieldAsync<UserType>(
                name: "changeUser",
                resolve: async context => await dbContext.User.FindAsync(context.Source.ChangeUserId));

            FieldAsync<UserType>(
                name: "registrationUser",
                resolve: async context => await dbContext.User.FindAsync(context.Source.RegistrationUserId));

            FieldAsync<ConstructionInvestorType>(
                name: "constructionInvestor",
                resolve: async context => await dbContext.ConstructionInvestors.FindAsync(context.Source.ConstructionInvestorId));

            FieldAsync<ExpenseType>(
                name: "expense",
                resolve: async context => await dbContext.Expenses.FindAsync(context.Source.ExpenseId));


            FieldAsync<ConstructionType>(
                name: "construction",
                resolve: async context => await dbContext.Constructions.FindAsync(context.Source.ConstructionId));
        }
    }
}
