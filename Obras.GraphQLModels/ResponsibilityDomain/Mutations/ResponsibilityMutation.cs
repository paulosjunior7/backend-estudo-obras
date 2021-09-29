using GraphQL;
using GraphQL.Types;
using Obras.Business.ResponsibilityDomain.Models;
using Obras.Business.ResponsibilityDomain.Services;
using Obras.Data;
using Obras.GraphQLModels.ResponsibilityDomain.InputTypes;
using Obras.GraphQLModels.ResponsibilityDomain.Types;

namespace Obras.GraphQLModels.ResponsibilityDomain.Mutations
{
    public class ResponsibilityMutation : ObjectGraphType
    {
        public ResponsibilityMutation(IResponsibilityService responsibilityService, ObrasDBContext dBContext)
        {
            Name = nameof(ResponsibilityMutation);

            FieldAsync<ResponsibilityType>(
                name: "createResponsibility",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ResponsibilityInputType>> { Name = "responsibility" }),
                resolve: async context =>
                {
                    var responsibilityModel = context.GetArgument<ResponsibilityModel>("responsibility");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);

                    responsibilityModel.CompanyId = (int)(responsibilityModel.CompanyId == null ? user.CompanyId != null ? user.CompanyId : 0 : responsibilityModel.CompanyId);
                    responsibilityModel.ChangeUserId = userId;
                    responsibilityModel.RegistrationUserId = userId;

                    var responsibility = await responsibilityService.CreateAsync(responsibilityModel);
                    return responsibility;
                });

            FieldAsync<ResponsibilityType>(
                name: "updateResponsibility",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<ResponsibilityInputType>> { Name = "responsibility" }),
                resolve: async context =>
                {
                    int responsibilityId = context.GetArgument<int>("id");
                    var responsibilityModel = context.GetArgument<ResponsibilityModel>("responsibility");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);

                    responsibilityModel.CompanyId = (int)(responsibilityModel.CompanyId == null ? user.CompanyId != null ? user.CompanyId : 0 : responsibilityModel.CompanyId);
                    responsibilityModel.ChangeUserId = userId;

                    return await responsibilityService.UpdateResponsibilityAsync(responsibilityId, responsibilityModel);
                });
        }
    }
}
