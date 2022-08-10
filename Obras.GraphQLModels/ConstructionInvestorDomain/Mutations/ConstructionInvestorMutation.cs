using GraphQL;
using GraphQL.Types;
using Obras.Business.ConstructionInvestorDomain.Models;
using Obras.Business.ConstructionInvestorDomain.Services;
using Obras.Data;
using Obras.GraphQLModels.ConstructionInvestorDomain.InputTypes;
using Obras.GraphQLModels.ConstructionInvestorDomain.Types;

namespace Obras.GraphQLModels.ConstructionInvestorDomain.Mutations
{
    public class ConstructionInvestorMutation : ObjectGraphType
    {
        public ConstructionInvestorMutation(IConstructionInvestorService service, ObrasDBContext dBContext)
        {
            Name = nameof(ConstructionInvestorMutation);

            this.AuthorizeWith("LoggedIn");

            FieldAsync<ConstructionInvestorType>(
                name: "createConstructionInvestor",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ConstructionInvestorInputType>> { Name = "constructionInvestor" }),
                resolve: async context =>
                {
                    var model = context.GetArgument<ConstructionInvestorModel>("constructionInvestor");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    model.ChangeUserId = userId;
                    model.RegistrationUserId = userId;

                    var constructionInvestor = await service.CreateAsync(model);
                    return constructionInvestor;
                });

            FieldAsync<ConstructionInvestorType>(
                name: "updateConstructionInvestor",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<ConstructionInvestorInputType>> { Name = "constructionInvestor" }),
                resolve: async context =>
                {
                    int id = context.GetArgument<int>("id");
                    var model = context.GetArgument<ConstructionInvestorModel>("constructionInvestor");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    model.ChangeUserId = userId;

                    return await service.UpdateAsync(id, model);
                });
        }
    }
}
