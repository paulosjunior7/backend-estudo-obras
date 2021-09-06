namespace Obras.GraphQLModels.Schemas
{
    using Obras.GraphQLModels.Mutations;
    using Obras.GraphQLModels.Queries;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ObrasSchema : GraphQL.Types.Schema
    {
        public ObrasSchema(IServiceProvider services) : base(services)
        {
            Query = (ObrasQuery)services.GetService(typeof(ObrasQuery));
            Mutation = (ObrasMutation)services.GetService(typeof(ObrasMutation));
        }
    }
}
