namespace Obras.GraphQLModels.ProviderDomain.Mutations
{
    using GraphQL;
    using GraphQL.Types;
    using Obras.Business.ProviderDomain.Models;
    using Obras.Business.ProviderDomain.Services;
    using Obras.Data;
    using Obras.GraphQLModels.ProviderDomain.InputTypes;
    using Obras.GraphQLModels.ProviderDomain.Types;

    public class ProviderMutation : ObjectGraphType
    {
        public ProviderMutation(IProviderService providerService, ObrasDBContext dBContext)
        {
            Name = nameof(ProviderMutation);
            this.AuthorizeWith("LoggedIn");


            FieldAsync<ProviderType>(
                name: "createProvider",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ProviderInputType>> { Name = "provider" }),
                resolve: async context =>
                {
                    var providerModel = context.GetArgument<ProviderModel>("provider");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);

                    providerModel.ChangeUserId = userId;
                    providerModel.RegistrationUserId = userId;
                    providerModel.CompanyId = (int)(providerModel.CompanyId == null ? user.CompanyId != null ? user.CompanyId : 0 : providerModel.CompanyId);

                    var provider = await providerService.CreateAsync(providerModel);
                    return provider;
                });

            FieldAsync<ProviderType>(
                name: "updateProvider",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<ProviderInputType>> { Name = "provider" }),
                resolve: async context =>
                {
                    int providerId = context.GetArgument<int>("id");
                    var providerModel = context.GetArgument<ProviderModel>("provider");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);

                    providerModel.ChangeUserId = userId;
                    providerModel.CompanyId = (int)(providerModel.CompanyId == null ? user.CompanyId != null ? user.CompanyId : 0 : providerModel.CompanyId);

                    return await providerService.UpdateProviderAsync(providerId, providerModel);
                });
        }
    }
}
