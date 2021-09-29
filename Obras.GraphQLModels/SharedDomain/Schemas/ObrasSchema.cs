namespace Obras.GraphQLModels.SharedDomain.Schemas
{
    using Obras.GraphQLModels.SharedDomain.Mutations;
    using Obras.GraphQLModels.SharedDomain.Queries;
    using System;

    public class ObrasSchema : GraphQL.Types.Schema
    {
        public ObrasSchema(IServiceProvider services) : base(services)
        {
            Query = (ObrasQuery)services.GetService(typeof(ObrasQuery));
            Mutation = (ObrasMutation)services.GetService(typeof(ObrasMutation));
        }
    }
}
