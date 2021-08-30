namespace Obras.GraphQLModels.Schemas
{
    using Obras.GraphQLModels.Mutations;
    using Obras.GraphQLModels.Queries;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CompanySchema : GraphQL.Types.Schema
    {
        public CompanySchema(IServiceProvider services) : base(services)
        {
            Query = (CompanyQuery)services.GetService(typeof(CompanyQuery));
            Mutation = (CompanyMutation)services.GetService(typeof(CompanyMutation));
        }
    }
}
