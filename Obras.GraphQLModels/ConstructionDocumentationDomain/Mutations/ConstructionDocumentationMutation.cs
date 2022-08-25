using GraphQL;
using GraphQL.Types;
using Obras.Business.ConstructionDocumentationDomain.Models;
using Obras.Business.ConstructionDocumentationDomain.Services;
using Obras.Data;
using Obras.GraphQLModels.ConstructionDocumentationDomain.InputTypes;
using Obras.GraphQLModels.ConstructionDocumentationDomain.Types;

namespace Obras.GraphQLModels.ConstructionDocumentationDomain.Mutations
{
    public class ConstructionDocumentationMutation : ObjectGraphType
    {
        public ConstructionDocumentationMutation(IConstructionDocumentationService service, ObrasDBContext dBContext)
        {
            Name = nameof(ConstructionDocumentationMutation);

            this.AuthorizeWith("LoggedIn");

            FieldAsync<ConstructionDocumentationType>(
                name: "createConstructionDocumentation",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ConstructionDocumentationInputType>> { Name = "constructionDocumentation" }),
                resolve: async context =>
                {
                    var model = context.GetArgument<ConstructionDocumentationModel>("constructionDocumentation");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    model.ChangeUserId = userId;
                    model.RegistrationUserId = userId;

                    var constructionManpower = await service.CreateAsync(model);
                    return constructionManpower;
                });

            FieldAsync<ConstructionDocumentationType>(
                name: "updateConstructionDocumentation",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<ConstructionDocumentationInputType>> { Name = "constructionDocumentation" }),
                resolve: async context =>
                {
                    int id = context.GetArgument<int>("id");
                    var model = context.GetArgument<ConstructionDocumentationModel>("constructionDocumentation");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    model.ChangeUserId = userId;

                    return await service.UpdateAsync(id, model);
                });
        }
    }
}
