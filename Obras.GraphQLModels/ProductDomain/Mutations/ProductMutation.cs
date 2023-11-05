namespace Obras.GraphQLModels.ProductDomain.Mutations
{
    using GraphQL;
    using GraphQL.Types;
    using Obras.Business.ProductDomain.Models;
    using Obras.Business.ProductDomain.Services;
    using Obras.Data;
    using Obras.GraphQLModels.ProductDomain.InputTypes;
    using Obras.GraphQLModels.ProductDomain.Types;

    public class ProductMutation : ObjectGraphType
    {
        public ProductMutation(IProductService productService, ObrasDBContext dBContext)
        {
            Name = nameof(ProductMutation);

            this.AuthorizeWith("LoggedIn");

            FieldAsync<ProductType>(
                name: "createProduct",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ProductInputType>> { Name = "product" }),
                resolve: async context =>
                {
                    var productModel = context.GetArgument<ProductModel>("product");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    if (userId == null)
                    throw new ExecutionError("Verifique o token!");

                    var user = await dBContext.User.FindAsync(userId);
                    if (user == null || user.CompanyId == null)
                    throw new ExecutionError("Usuário não exite ou não possui empresa vinculada!");

                    productModel.CompanyId = (int)(productModel.CompanyId == null ? user.CompanyId != null ? user.CompanyId : 0 : productModel.CompanyId);
                    productModel.ChangeUserId = userId;
                    productModel.RegistrationUserId = userId;

                    var company = await productService.CreateAsync(productModel);
                    return company;
                });

            FieldAsync<ProductType>(
                name: "updateProduct",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<ProductInputType>> { Name = "product" }),
                resolve: async context =>
                {
                    int productId = context.GetArgument<int>("id");
                    var productModel = context.GetArgument<ProductModel>("product");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);

                    productModel.CompanyId = (int)(productModel.CompanyId == null ? user.CompanyId != null ? user.CompanyId : 0 : productModel.CompanyId);
                    productModel.ChangeUserId = userId;

                    return await productService.UpdateProductAsync(productId, productModel);
                });
        }
    }
}
