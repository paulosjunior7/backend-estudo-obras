namespace Obras.GraphQLModels.Mutations
{
    using GraphQL.Types;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ObrasMutation : ObjectGraphType
    {
        public ObrasMutation()
        {
            Name = nameof(ObrasMutation);
            Field<CompanyMutation>("companies", resolve: context => new { });
            Field<ProductMutation>("products", resolve: context => new { });
            Field<ProviderMutation>("providers", resolve: context => new { });
            Field<BrandMutation>("brands", resolve: context => new { });
        }
    }
}
