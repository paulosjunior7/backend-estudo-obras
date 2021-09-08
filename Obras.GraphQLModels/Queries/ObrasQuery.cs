using GraphQL.Types;
using Obras.Business.Services;

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
        }
    }
}
