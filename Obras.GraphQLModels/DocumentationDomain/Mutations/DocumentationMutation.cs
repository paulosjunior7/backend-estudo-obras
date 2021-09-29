using GraphQL;
using GraphQL.Types;
using Obras.Business.DocumentationDomain.Models;
using Obras.Business.DocumentationDomain.Services;
using Obras.Data;
using Obras.GraphQLModels.DocumentationDomain.InputTypes;
using Obras.GraphQLModels.DocumentationDomain.Types;

namespace Obras.GraphQLModels.DocumentationDomain.Mutations
{
    public class DocumentationMutation : ObjectGraphType
    {
        public DocumentationMutation(IDocumentationService documentationService, ObrasDBContext dBContext)
        {
            Name = nameof(DocumentationMutation);

            FieldAsync<DocumentationType>(
                name: "createDocumentation",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<DocumentationInputType>> { Name = "documentation" }),
                resolve: async context =>
                {
                    var documentationModel = context.GetArgument<DocumentationModel>("documentation");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);

                    documentationModel.CompanyId = (int)(documentationModel.CompanyId == null ? user.CompanyId != null ? user.CompanyId : 0 : documentationModel.CompanyId);
                    documentationModel.ChangeUserId = userId;
                    documentationModel.RegistrationUserId = userId;

                    var documentation = await documentationService.CreateAsync(documentationModel);
                    return documentation;
                });

            FieldAsync<DocumentationType>(
                name: "updateDocumentation",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<DocumentationInputType>> { Name = "documentation" }),
                resolve: async context =>
                {
                    int documentationId = context.GetArgument<int>("id");
                    var documentationModel = context.GetArgument<DocumentationModel>("documentation");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);

                    documentationModel.CompanyId = (int)(documentationModel.CompanyId == null ? user.CompanyId != null ? user.CompanyId : 0 : documentationModel.CompanyId);
                    documentationModel.ChangeUserId = userId;

                    return await documentationService.UpdateDocumentationAsync(documentationId, documentationModel);
                });
        }
    }
}
