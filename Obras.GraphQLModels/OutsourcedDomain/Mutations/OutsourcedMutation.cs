using GraphQL;
using GraphQL.Types;
using Obras.Business.OutsourcedDomain.Models;
using Obras.Business.OutsourcedDomain.Services;
using Obras.Data;
using Obras.GraphQLModels.OutsourcedDomain.InputTypes;
using Obras.GraphQLModels.OutsourcedDomain.Types;

namespace Obras.GraphQLModels.OutsourcedDomain.Mutations
{
    public class OutsourcedMutation : ObjectGraphType
    {
        public OutsourcedMutation(IOutsourcedService outsourcedService, ObrasDBContext dBContext)
        {
            Name = nameof(OutsourcedMutation);

            this.AuthorizeWith("LoggedIn");

            FieldAsync<OutsourcedType>(
                name: "createOutsourced",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<OutsourcedInputType>> { Name = "outsourced" }),
                resolve: async context =>
                {
                    var outsourcedModel = context.GetArgument<OutsourcedModel>("outsourced");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);

                    outsourcedModel.CompanyId = (int)(outsourcedModel.CompanyId == null ? user.CompanyId != null ? user.CompanyId : 0 : outsourcedModel.CompanyId);
                    outsourcedModel.ChangeUserId = userId;
                    outsourcedModel.RegistrationUserId = userId;

                    var expense = await outsourcedService.CreateAsync(outsourcedModel);
                    return expense;
                });

            FieldAsync<OutsourcedType>(
                name: "updateOutsourced",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<OutsourcedInputType>> { Name = "outsourced" }),
                resolve: async context =>
                {
                    int id = context.GetArgument<int>("id");
                    var model = context.GetArgument<OutsourcedModel>("outsourced");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);

                    model.CompanyId = (int)(model.CompanyId == null ? user.CompanyId != null ? user.CompanyId : 0 : model.CompanyId);
                    model.ChangeUserId = userId;

                    return await outsourcedService.UpdateOutsourcedAsync(id, model);
                });
        }
    }
}
