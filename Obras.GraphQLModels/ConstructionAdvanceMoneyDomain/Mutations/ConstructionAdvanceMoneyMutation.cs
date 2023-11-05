using GraphQL;
using GraphQL.Types;
using Obras.Business.ConstructionAdvanceMoneyDomain.Models;
using Obras.Business.ConstructionAdvanceMoneyDomain.Services;
using Obras.Data;
using Obras.GraphQLModels.ConstructionAdvanceMoneyDomain.InputTypes;
using Obras.GraphQLModels.ConstructionAdvanceMoneyDomain.Types;

namespace Obras.GraphQLModels.ConstructionAdvanceMoneyDomain.Mutations
{
    public class ConstructionAdvanceMoneyMutation : ObjectGraphType
    {
        public ConstructionAdvanceMoneyMutation(IConstructionAdvanceMoneyService service, ObrasDBContext dBContext)
        {
            Name = nameof(ConstructionAdvanceMoneyMutation);

            this.AuthorizeWith("LoggedIn");

            FieldAsync<ConstructionAdvanceMoneyType>(
                name: "createConstructionAdvanceMoney",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ConstructionAdvanceMoneyInputType>> { Name = "constructionAdvanceMoney" }),
                resolve: async context =>
                {
                    var model = context.GetArgument<ConstructionAdvanceMoneyModel>("constructionAdvanceMoney");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    if (userId == null)
                    throw new ExecutionError("Verifique o token!");

                    var user = await dBContext.User.FindAsync(userId);
                    if (user == null || user.CompanyId == null)
                    throw new ExecutionError("Usuário não exite ou não possui empresa vinculada!");

                    model.ChangeUserId = userId;
                    model.RegistrationUserId = userId;

                    var constructionAdvanceMoney = await service.CreateAsync(model);
                    return constructionAdvanceMoney;
                });

            FieldAsync<ConstructionAdvanceMoneyType>(
                name: "updateConstructionAdvanceMoney",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<ConstructionAdvanceMoneyInputType>> { Name = "constructionAdvanceMoney" }),
                resolve: async context =>
                {
                    int id = context.GetArgument<int>("id");
                    var model = context.GetArgument<ConstructionAdvanceMoneyModel>("constructionAdvanceMoney");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    model.ChangeUserId = userId;

                    return await service.UpdateAsync(id, model);
                });
        }
    }
}
