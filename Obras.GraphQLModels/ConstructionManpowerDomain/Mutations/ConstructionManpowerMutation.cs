using GraphQL;
using GraphQL.Types;
using Obras.Business.ConstructionManpowerDomain.Models;
using Obras.Business.ConstructionManpowerDomain.Services;
using Obras.Data;
using Obras.GraphQLModels.ConstructionManpowerDomain.InputTypes;
using Obras.GraphQLModels.ConstructionManpowerDomain.Types;

namespace Obras.GraphQLModels.ConstructionManpowerDomain.Mutations
{
    public class ConstructionManpowerMutation : ObjectGraphType
    {
        public ConstructionManpowerMutation(IConstructionManpowerService service, ObrasDBContext dBContext)
        {
            Name = nameof(ConstructionManpowerMutation);

            this.AuthorizeWith("LoggedIn");

            FieldAsync<ConstructionManpowerType>(
                name: "createConstructionManpower",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ConstructionManpowerInputType>> { Name = "constructionManpower" }),
                resolve: async context =>
                {
                    var model = context.GetArgument<ConstructionManpowerModel>("constructionManpower");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    model.ChangeUserId = userId;
                    model.RegistrationUserId = userId;

                    var constructionManpower = await service.CreateAsync(model);
                    return constructionManpower;
                });

            FieldAsync<ConstructionManpowerType>(
                name: "updateConstructionManpower",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<ConstructionManpowerInputType>> { Name = "constructionManpower" }),
                resolve: async context =>
                {
                    int id = context.GetArgument<int>("id");
                    var model = context.GetArgument<ConstructionManpowerModel>("constructionManpower");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    model.ChangeUserId = userId;

                    return await service.UpdateAsync(id, model);
                });
        }
    }
}
