using GraphQL;
using GraphQL.Types;
using Obras.Business.ConstructionHouseDomain.Models;
using Obras.Business.ConstructionHouseDomain.Services;
using Obras.Data;
using Obras.GraphQLModels.ConstructionHouseDomain.InputTypes;
using Obras.GraphQLModels.ConstructionHouseDomain.Types;

namespace Obras.GraphQLModels.ConstructionHouseDomain.Mutations
{
    public class ConstructionHouseMutation : ObjectGraphType
    {
        public ConstructionHouseMutation(IConstructionHouseService service, ObrasDBContext dBContext)
        {
            Name = nameof(ConstructionHouseMutation);

            this.AuthorizeWith("LoggedIn");

            FieldAsync<ConstructionHouseType>(
                name: "createConstructionHouse",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ConstructionHouseInputType>> { Name = "constructionHouse" }),
                resolve: async context =>
                {
                    var model = context.GetArgument<ConstructionHouseModel>("constructionHouse");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    model.ChangeUserId = userId;
                    model.RegistrationUserId = userId;

                    var constructionInvestor = await service.CreateAsync(model);
                    return constructionInvestor;
                });

            FieldAsync<ConstructionHouseType>(
                name: "updateConstructionHouse",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<ConstructionHouseInputType>> { Name = "constructionHouse" }),
                resolve: async context =>
                {
                    int id = context.GetArgument<int>("id");
                    var model = context.GetArgument<ConstructionHouseModel>("constructionHouse");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    model.ChangeUserId = userId;

                    return await service.UpdateAsync(id, model);
                });
        }
    }
}
