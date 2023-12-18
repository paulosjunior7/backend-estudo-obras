using GraphQL;
using GraphQL.Types;
using Obras.Business.UnitDomain.Services;
using Obras.Data;
using Obras.GraphQLModels.SharedDomain.Types;

namespace Obras.GraphQLModels.UnityDomain.Queries
{
    public class UserQuery : ObjectGraphType
    {
        public UserQuery(ObrasDBContext dBContext)
        {
            FieldAsync<UserType>(
                "findMe",
                arguments: new QueryArguments(),
                resolve: async context =>
                {
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    if (userId == null)
                        throw new ExecutionError("Verifique o token!");

                    var user = await dBContext.User.FindAsync(userId);
                    if (user == null || user.CompanyId == null)
                        throw new ExecutionError("Usuário não exite ou não possui empresa vinculada!");

                    return user;
                });
        }
    }
}
