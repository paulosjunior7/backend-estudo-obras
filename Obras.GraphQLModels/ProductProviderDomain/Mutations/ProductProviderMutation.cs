using GraphQL;
using GraphQL.Types;
using Obras.Business.ProductProviderDomain.Models;
using Obras.Business.ProductProviderDomain.Services;
using Obras.Data;
using Obras.GraphQLModels.ProductProviderDomain.InputTypes;
using Obras.GraphQLModels.ProductProviderDomain.Types;

namespace Obras.GraphQLModels.ProductProviderDomain.Mutations
{
    public class ProductProviderMutation : ObjectGraphType
    {
        public ProductProviderMutation(IProductProviderService productProviderService, ObrasDBContext dBContext)
        {
            Name = nameof(ProductProviderMutation);

            this.AuthorizeWith("LoggedIn");

            FieldAsync<ProductProviderType>(
                name: "createProductProvider",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ProductProviderInputType>> { Name = "productProvider" }),
                resolve: async context =>
                {
                    var productModel = context.GetArgument<ProductProviderModel>("productProvider");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);

                    productModel.ChangeUserId = userId;
                    productModel.RegistrationUserId = userId;

                    var company = await productProviderService.CreateAsync(productModel);
                    return company;
                });

            FieldAsync<ProductProviderType>(
                name: "updateProductProvider",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<ProductProviderInputType>> { Name = "productProvider" }),
                resolve: async context =>
                {
                    int productProviderId = context.GetArgument<int>("id");
                    var productModel = context.GetArgument<ProductProviderModel>("productProvider");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);

                    productModel.ChangeUserId = userId;

                    return await productProviderService.UpdateProductProviderAsync(productProviderId, productModel);
                });
        }
    }
}
