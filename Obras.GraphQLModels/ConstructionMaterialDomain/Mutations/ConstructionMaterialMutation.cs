using GraphQL;
using GraphQL.Types;
using Obras.Business.ConstructionMaterialDomain.Models;
using Obras.Business.ConstructionMaterialDomain.Services;
using Obras.Data;
using Obras.GraphQLModels.ConstructionMaterialDomain.InputTypes;
using Obras.GraphQLModels.ConstructionMaterialDomain.Types;

namespace Obras.GraphQLModels.ConstructionMaterialDomain.Mutations
{
    public class ConstructionMaterialMutation : ObjectGraphType
    {
        public ConstructionMaterialMutation(IConstructionMaterialService service, ObrasDBContext dBContext)
        {
            Name = nameof(ConstructionMaterialMutation);

            this.AuthorizeWith("LoggedIn");

            FieldAsync<ConstructionMaterialType>(
                name: "createConstructionMaterial",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ConstructionMaterialInputType>> { Name = "constructionMaterial" }),
                resolve: async context =>
                {
                    var model = context.GetArgument<ConstructionMaterialModel>("constructionMaterial");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    model.ChangeUserId = userId;
                    model.RegistrationUserId = userId;

                    var constructionMaterial = await service.CreateAsync(model);
                    return constructionMaterial;
                });

            FieldAsync<ConstructionMaterialType>(
                name: "updateConstructionMaterial",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<ConstructionMaterialInputType>> { Name = "constructionMaterial" }),
                resolve: async context =>
                {
                    int id = context.GetArgument<int>("id");
                    var model = context.GetArgument<ConstructionMaterialModel>("constructionMaterial");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    model.ChangeUserId = userId;

                    return await service.UpdateAsync(id, model);
                });
        }
    }
}
