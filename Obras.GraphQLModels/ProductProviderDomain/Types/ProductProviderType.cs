using GraphQL.Types;
using Obras.Data;
using Obras.Data.Entities;
using Obras.GraphQLModels.ProductDomain.Types;
using Obras.GraphQLModels.ProviderDomain.Types;
using Obras.GraphQLModels.SharedDomain.Types;

namespace Obras.GraphQLModels.ProductProviderDomain.Types
{
    public class ProductProviderType : ObjectGraphType<ProductProvider>
    {
        public ProductProviderType(ObrasDBContext dbContext)
        {
            Name = nameof(ProductProviderType);

            Field(x => x.Id);
            Field(x => x.AuxiliaryCode);
            Field(x => x.CreationDate, nullable: true);
            Field(x => x.ChangeDate, nullable: true);
            Field(x => x.Active);

            FieldAsync<UserType>(
                name: "changeUser",
                resolve: async context => await dbContext.User.FindAsync(context.Source.ChangeUserId));

            FieldAsync<UserType>(
                name: "registrationUser",
                resolve: async context => await dbContext.User.FindAsync(context.Source.RegistrationUserId));

            FieldAsync<ProductType>(
                name: "product",
                resolve: async context => await dbContext.Products.FindAsync(context.Source.ProductId));

            FieldAsync<ProviderType>(
                name: "provider",
                resolve: async context => await dbContext.Providers.FindAsync(context.Source.ProviderId));
        }
    }
}
