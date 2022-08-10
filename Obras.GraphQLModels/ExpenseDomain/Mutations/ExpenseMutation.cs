using GraphQL;
using GraphQL.Types;
using Obras.Business.ExpenseDomain.Models;
using Obras.Business.ExpenseDomain.Services;
using Obras.Data;
using Obras.GraphQLModels.ExpenseDomain.InputTypes;
using Obras.GraphQLModels.ExpenseDomain.Types;

namespace Obras.GraphQLModels.ExpenseDomain.Mutations
{
    public class ExpenseMutation : ObjectGraphType
    {
        public ExpenseMutation(IExpenseService expenseService, ObrasDBContext dBContext)
        {
            Name = nameof(ExpenseMutation);

            this.AuthorizeWith("LoggedIn");

            FieldAsync<ExpenseType>(
                name: "createExpense",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ExpenseInputType>> { Name = "expense" }),
                resolve: async context =>
                {
                    var expenseModel = context.GetArgument<ExpenseModel>("expense");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);

                    expenseModel.CompanyId = (int)(expenseModel.CompanyId == null ? user.CompanyId != null ? user.CompanyId : 0 : expenseModel.CompanyId);
                    expenseModel.ChangeUserId = userId;
                    expenseModel.RegistrationUserId = userId;

                    var expense = await expenseService.CreateAsync(expenseModel);
                    return expense;
                });

            FieldAsync<ExpenseType>(
                name: "updateExpense",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<ExpenseInputType>> { Name = "expense" }),
                resolve: async context =>
                {
                    int expenseId = context.GetArgument<int>("id");
                    var expenseModel = context.GetArgument<ExpenseModel>("expense");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);

                    expenseModel.CompanyId = (int)(expenseModel.CompanyId == null ? user.CompanyId != null ? user.CompanyId : 0 : expenseModel.CompanyId);
                    expenseModel.ChangeUserId = userId;

                    return await expenseService.UpdateExpenseAsync(expenseId, expenseModel);
                });
        }
    }
}
