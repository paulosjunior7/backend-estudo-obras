using GraphQL;
using GraphQL.Types;
using Obras.Business.UnitDomain.Models;
using Obras.Business.UnitDomain.Services;
using Obras.Data;
using Obras.GraphQLModels.ResponsibilityDomain.InputTypes;
using Obras.GraphQLModels.UnityDomain.InputTypes;
using Obras.GraphQLModels.UnityDomain.Types;

namespace Obras.GraphQLModels.UnityDomain.Mutations
{
    public class UnityMutation : ObjectGraphType
    {
        public UnityMutation(IUnityService service, ObrasDBContext dBContext)
        {
            Name = nameof(UnityMutation);

            this.AuthorizeWith("LoggedIn");

            FieldAsync<UnityType>(
                name: "createUnity",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<UnityInputType>> { Name = "unity" }),
                resolve: async context =>
                {
                    var model = context.GetArgument<UnityModel>("unity");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    if (userId == null)
                        throw new ExecutionError("Verifique o token!");

                    var user = await dBContext.User.FindAsync(userId);
                    if (user == null || user.CompanyId == null)
                        throw new ExecutionError("Usuário não exite ou não possui empresa vinculada!");


                    model.CompanyId = (int)(model.CompanyId == null ? user.CompanyId != null ? user.CompanyId : 0 : model.CompanyId);
                    model.ChangeUserId = userId;
                    model.RegistrationUserId = userId;

                    var unity = await service.CreateAsync(model);
                    return unity;
                });

            FieldAsync<UnityType>(
                name: "updateUnity",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<UnityInputType>> { Name = "unity" }),
                resolve: async context =>
                {
                    int id = context.GetArgument<int>("id");
                    var model = context.GetArgument<UnityModel>("unity");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    if (userId == null)
                        throw new ExecutionError("Verifique o token!");

                    var user = await dBContext.User.FindAsync(userId);
                    if (user == null || user.CompanyId == null)
                        throw new ExecutionError("Usuário não exite ou não possui empresa vinculada!");


                    model.CompanyId = (int)(model.CompanyId == null ? user.CompanyId != null ? user.CompanyId : 0 : model.CompanyId);
                    model.ChangeUserId = userId;

                    return await service.UpdateAsync(user.CompanyId, id, model);
                });
        }
    }
}
