namespace Obras.GraphQLModels.Mutations
{
    using GraphQL;
    using GraphQL.Types;
    using Obras.Business.Models;
    using Obras.Business.Services;
    using Obras.Data;
    using Obras.GraphQLModels.InputTypes;
    using Obras.GraphQLModels.Types;
    using System;
    using System.Collections.Generic;
    using System.Text;

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

                    var user = await dBContext.User.FindAsync(userId);

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
