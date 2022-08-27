using GraphQL;
using GraphQL.Types;
using Obras.Business.ConstructionExpenseDomain.Models;
using Obras.Business.ConstructionExpenseDomain.Services;
using Obras.Data;
using Obras.GraphQLModels.ConstructionExpenseDomain.InputTypes;
using Obras.GraphQLModels.ConstructionExpenseDomain.Types;

namespace Obras.GraphQLModels.ConstructionExpenseDomain.Mutations
{
    public class ConstructionExpenseMutation : ObjectGraphType
    {
        public ConstructionExpenseMutation(IConstructionExpenseService service, ObrasDBContext dBContext)
        {
            Name = nameof(ConstructionExpenseMutation);

            this.AuthorizeWith("LoggedIn");

            FieldAsync<ConstructionExpenseType>(
                name: "createConstructionExpense",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ConstructionExpenseInputType>> { Name = "constructionExpense" }),
                resolve: async context =>
                {
                    var model = context.GetArgument<ConstructionExpenseModel>("constructionExpense");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    model.ChangeUserId = userId;
                    model.RegistrationUserId = userId;

                    var constructionManpower = await service.CreateAsync(model);
                    return constructionManpower;
                });

            FieldAsync<ConstructionExpenseType>(
                name: "updateConstructionExpense",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<ConstructionExpenseInputType>> { Name = "constructionExpense" }),
                resolve: async context =>
                {
                    int id = context.GetArgument<int>("id");
                    var model = context.GetArgument<ConstructionExpenseModel>("constructionExpense");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    model.ChangeUserId = userId;

                    return await service.UpdateAsync(id, model);
                });
        }
    }
}
