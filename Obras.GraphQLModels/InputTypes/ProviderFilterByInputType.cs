namespace Obras.GraphQLModels.InputTypes
{
    using GraphQL.Types;
    using Obras.Business.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ProviderFilterByInputType : InputObjectGraphType<ProviderFilter>
    {
        public ProviderFilterByInputType()
        {
            Field(x => x.Id, nullable: true);
            Field(x => x.Cnpj, nullable: true);
            Field(x => x.Name, nullable: true);
            Field(x => x.Active, nullable: true);
            Field(x => x.City, nullable: true);
            Field(x => x.CompanyId, nullable: true);
            Field(x => x.Neighbourhood, nullable: true);
            Field(x => x.State, nullable: true);
        }
    }
}
