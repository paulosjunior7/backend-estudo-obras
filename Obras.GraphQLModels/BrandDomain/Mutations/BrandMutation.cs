using GraphQL;
using GraphQL.Types;
using Obras.Business.BrandDomain.Models;
using Obras.Business.BrandDomain.Services;
using Obras.Data;
using Obras.GraphQLModels.BrandDomain.InputTypes;
using Obras.GraphQLModels.BrandDomain.Types;

namespace Obras.GraphQLModels.BrandDomain.Mutations
{
    public class BrandMutation : ObjectGraphType
    {
        public BrandMutation(IBrandService brandService, ObrasDBContext dBContext)
        {
            Name = nameof(BrandMutation);

            FieldAsync<BrandType>(
                name: "createBrand",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<BrandInputType>> { Name = "brand" }),
                resolve: async context =>
                {
                    var brandModel = context.GetArgument<BrandModel>("brand");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);

                    brandModel.CompanyId = (int)(brandModel.CompanyId == null ? user.CompanyId != null ? user.CompanyId : 0 : brandModel.CompanyId);
                    brandModel.ChangeUserId = userId;
                    brandModel.RegistrationUserId = userId;

                    var brand = await brandService.CreateAsync(brandModel);
                    return brand;
                });

            FieldAsync<BrandType>(
                name: "updateBrand",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<BrandInputType>> { Name = "brand" }),
                resolve: async context =>
                {
                    int brandId = context.GetArgument<int>("id");
                    var brandModel = context.GetArgument<BrandModel>("brand");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);

                    brandModel.CompanyId = (int)(brandModel.CompanyId == null ? user.CompanyId != null ? user.CompanyId : 0 : brandModel.CompanyId);
                    brandModel.ChangeUserId = userId;

                    return await brandService.UpdateBrandAsync(brandId, brandModel);
                });
        }
    }
}
