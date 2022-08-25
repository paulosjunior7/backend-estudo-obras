using GraphQL;
using GraphQL.Types;
using Obras.Business.ConstructionDomain.Models;
using Obras.Business.ConstructionDomain.Services;
using Obras.Data;
using Obras.GraphQLModels.ConstructionDomain.InputTypes;
using Obras.GraphQLModels.ConstructionDomain.Types;

namespace Obras.GraphQLModels.ConstructionDomain.Mutations
{
    public class ConstructionMutation : ObjectGraphType
    {
        public ConstructionMutation(IConstructionService service, ObrasDBContext dBContext)
        {
            Name = nameof(ConstructionMutation);

            this.AuthorizeWith("LoggedIn");

            FieldAsync<ConstructionType>(
                name: "createConstruction",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ConstructionInputType>> { Name = "construction" }),
                resolve: async context =>
                {
                    var model = context.GetArgument<ConstructionModel>("construction");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);

                    model.CompanyId = (int)(model.CompanyId == null || model.CompanyId == 0 ? user.CompanyId != null ? user.CompanyId : 0 : model.CompanyId);
                    model.ChangeUserId = userId;
                    model.RegistrationUserId = userId;

                    var company = await service.CreateAsync(model);
                    return company;
                });

            FieldAsync<ConstructionType>(
                name: "updateConstruction",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<ConstructionInputType>> { Name = "construction" }),
                resolve: async context =>
                {
                    int id = context.GetArgument<int>("id");
                    var model = context.GetArgument<ConstructionModel>("construction");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);

                    model.CompanyId = (int)(model.CompanyId == null || model.CompanyId == 0 ? user.CompanyId != null ? user.CompanyId : 0 : model.CompanyId);
                    model.ChangeUserId = userId;

                    return await service.UpdateAsync(id, model);
                });
        }
    }
}
