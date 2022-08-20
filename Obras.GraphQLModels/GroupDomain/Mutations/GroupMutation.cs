using GraphQL;
using GraphQL.Types;
using Obras.Business.GroupDomain.Models;
using Obras.Business.GroupDomain.Services;
using Obras.Data;
using Obras.GraphQLModels.GroupDomain.InputTypes;
using Obras.GraphQLModels.GroupDomain.Types;

namespace Obras.GraphQLModels.GroupDomain.Mutations
{
    public class GroupMutation : ObjectGraphType
    {
        public GroupMutation(IGroupService service, ObrasDBContext dBContext)
        {
            Name = nameof(GroupMutation);

            this.AuthorizeWith("LoggedIn");

            FieldAsync<GroupType>(
                name: "createGroup",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<GroupInputType>> { Name = "group" }),
                resolve: async context =>
                {
                    var model = context.GetArgument<GroupModel>("group");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);

                    model.CompanyId = (int)(model.CompanyId == null ? user.CompanyId != null ? user.CompanyId : 0 : model.CompanyId);
                    model.ChangeUserId = userId;
                    model.RegistrationUserId = userId;

                    var brand = await service.CreateAsync(model);
                    return brand;
                });

            FieldAsync<GroupType>(
                name: "updateGroup",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<GroupInputType>> { Name = "group" }),
                resolve: async context =>
                {
                    int id = context.GetArgument<int>("id");
                    var model = context.GetArgument<GroupModel>("group");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);

                    model.CompanyId = (int)(model.CompanyId == null ? user.CompanyId != null ? user.CompanyId : 0 : model.CompanyId);
                    model.ChangeUserId = userId;

                    return await service.UpdateAsync(id, model);
                });
        }
    }
}
