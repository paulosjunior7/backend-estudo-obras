namespace Obras.GraphQLModels.InputTypes
{
    using GraphQL.Types;
    using Obras.Business.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CompanyFilterByInputType : InputObjectGraphType<CompanyFilter>
    {
        public CompanyFilterByInputType()
        {
            Field(x => x.Id, nullable: true);
            Field(x => x.Cnpj);
            Field(x => x.CorporateName);
            Field(x => x.Active, nullable: true);
            Field(x => x.City);
            Field(x => x.FantasyName);
            Field(x => x.Neighbourhood);
            Field(x => x.State);
        }
    }
}
