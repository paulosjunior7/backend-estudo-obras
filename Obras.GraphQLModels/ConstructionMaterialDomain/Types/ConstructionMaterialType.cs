namespace Obras.GraphQLModels.ConstructionMaterialDomain.Types
{
    using GraphQL.Types;
    using Obras.Data;
    using Obras.Data.Entities;
    using Obras.GraphQLModels.BrandDomain.Types;
    using Obras.GraphQLModels.ConstructionDomain.Types;
    using Obras.GraphQLModels.ConstructionInvestorDomain.Types;
    using Obras.GraphQLModels.GroupDomain.Types;
    using Obras.GraphQLModels.ProductDomain.Types;
    using Obras.GraphQLModels.ProviderDomain.Types;
    using Obras.GraphQLModels.SharedDomain.Types;
    using Obras.GraphQLModels.UnityDomain.Types;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ConstructionMaterialType : ObjectGraphType<ConstructionMaterial>
    {
        public ConstructionMaterialType(ObrasDBContext dbContext)
        {
            Name = nameof(ConstructionMaterialType);

            Field(x => x.Id);
            Field(x => x.BrandId, nullable: true);
            Field(x => x.ConstructionInvestorId, nullable: true);
            Field(x => x.GroupId, nullable: true);
            Field(x => x.ProductId, nullable: true);
            Field(x => x.ProviderId, nullable: true);
            Field(x => x.PurchaseDate, nullable: true);
            Field(x => x.Quantity, nullable: true);
            Field(x => x.UnitPrice, nullable: true);
            Field(x => x.ConstructionId, nullable: true);
            Field(x => x.Active);

            FieldAsync<UserType>(
                name: "changeUser",
                resolve: async context => await dbContext.User.FindAsync(context.Source.ChangeUserId));

            FieldAsync<UserType>(
                name: "registrationUser",
                resolve: async context => await dbContext.User.FindAsync(context.Source.RegistrationUserId));

            FieldAsync<ConstructionInvestorType>(
                name: "constructionInvestor",
                resolve: async context => await dbContext.ConstructionInvestors.FindAsync(context.Source.ConstructionId));

            FieldAsync<GroupType>(
                name: "group",
                resolve: async context => await dbContext.Groups.FindAsync(context.Source.GroupId));

            FieldAsync<ProductType>(
                name: "product",
                resolve: async context => await dbContext.Products.FindAsync(context.Source.ProductId));

            FieldAsync<BrandType>(
                name: "brand",
                resolve: async context => await dbContext.Brands.FindAsync(context.Source.BrandId));

            FieldAsync<ProviderType>(
                name: "provider",
                resolve: async context => await dbContext.Providers.FindAsync(context.Source.ProviderId));

            FieldAsync<UnityType>(
                name: "unity",
                resolve: async context => await dbContext.Unities.FindAsync(context.Source.UnityId));

            FieldAsync<ConstructionType>(
                name: "construction",
                resolve: async context => await dbContext.Constructions.FindAsync(context.Source.ConstructionId));
        }
    }
}
