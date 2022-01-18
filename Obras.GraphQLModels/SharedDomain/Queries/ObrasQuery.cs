using GraphQL.Types;
using Obras.GraphQLModels.BrandDomain.Queries;
using Obras.GraphQLModels.CompanyDomain.Queries;
using Obras.GraphQLModels.DocumentationDomain.Queries;
using Obras.GraphQLModels.EmployeeDomain.Queries;
using Obras.GraphQLModels.ExpenseDomain.Queries;
using Obras.GraphQLModels.PeopleDomainQueries;
using Obras.GraphQLModels.ProductDomain.Queries;
using Obras.GraphQLModels.ProviderDomain.Queries;
using Obras.GraphQLModels.ResponsibilityDomain.Queries;

namespace Obras.GraphQLModels.SharedDomain.Queries
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
            Field<EmployeeQuery>("employees", resolve: context => new { });
        }
    }
}
