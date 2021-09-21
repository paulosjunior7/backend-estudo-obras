using GraphQL.Types;

namespace Obras.GraphQLModels.Queries
{
    public class ObrasQuery : ObjectGraphType
    {
        public ObrasQuery()
        {
            Name = nameof(ObrasQuery);
            Field<CompanyQuery>("companies", resolve: context => new { });
            Field<ProductQuery>("products", resolve: context => new { });
            Field<ProviderQuery>("providers", resolve: context => new { });
            Field<BrandQuery>("brands", resolve: context => new { });
            Field<DocumentationQuery>("documentations", resolve: context => new { });
            Field<ResponsibilityQuery>("responsibilities", resolve: context => new { });
            Field<ExpenseQuery>("expenses", resolve: context => new { });
            Field<PeopleQuery>("peoples", resolve: context => new { });
        }
    }
}
