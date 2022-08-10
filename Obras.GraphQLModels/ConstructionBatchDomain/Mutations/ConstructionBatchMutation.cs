using GraphQL;
using GraphQL.Types;
using Obras.Business.ConstructionBatchDomain.Models;
using Obras.Business.ConstructionBatchDomain.Services;
using Obras.Data;
using Obras.GraphQLModels.ConstructionBatchDomain.InputTypes;
using Obras.GraphQLModels.ConstructionBatchDomain.Types;

namespace Obras.GraphQLModels.ConstructionBatchDomain.Mutations
{
    public class ConstructionBatchMutation : ObjectGraphType
    {
        public ConstructionBatchMutation(IConstructionBatchService service, ObrasDBContext dBContext)
        {
            Name = nameof(ConstructionBatchMutation);

            this.AuthorizeWith("LoggedIn");

            FieldAsync<ConstructionBatchType>(
                name: "createConstructionBatch",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ConstructionBatchInputType>> { Name = "constructionBatch" }),
                resolve: async context =>
                {
                    var model = context.GetArgument<ConstructionBatchModel>("constructionBatch");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    model.ChangeUserId = userId;
                    model.RegistrationUserId = userId;

                    var constructionBatch = await service.CreateAsync(model);
                    return constructionBatch;
                });

            FieldAsync<ConstructionBatchType>(
                name: "updateConstructionBatch",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<ConstructionBatchInputType>> { Name = "constructionBatch" }),
                resolve: async context =>
                {
                    int id = context.GetArgument<int>("id");
                    var model = context.GetArgument<ConstructionBatchModel>("constructionBatch");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    model.ChangeUserId = userId;

                    return await service.UpdateAsync(id, model);
                });
        }
    }
}
