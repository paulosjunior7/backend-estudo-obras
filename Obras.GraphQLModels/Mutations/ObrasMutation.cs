namespace Obras.GraphQLModels.Mutations
{
    using GraphQL.Types;

    public class ObrasMutation : ObjectGraphType
    {
        public ObrasMutation()
        {
            Name = nameof(ObrasMutation);
            Field<CompanyMutation>("companies", resolve: context => new { });
            Field<ProductMutation>("products", resolve: context => new { });
            Field<ProviderMutation>("providers", resolve: context => new { });
            Field<BrandMutation>("brands", resolve: context => new { });
            Field<DocumentationMutation>("documentations", resolve: context => new { });
            Field<ResponsibilityMutation>("responsibilities", resolve: context => new { });
            Field<ExpenseMutation>("expenses", resolve: context => new { });
            Field<PeopleMutation>("peoples", resolve: context => new { });
        }
    }
}
