namespace Obras.GraphQLModels.UnityDomain.InputTypes
{
    using GraphQL.Types;
    using Obras.Business.UnitDomain.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UnityFilterByInputType : InputObjectGraphType<UnityFilter>
    {
        public UnityFilterByInputType()
        {
            Field(x => x.CompanyId, nullable: true);
            Field(x => x.Description, nullable: true);
            Field(x => x.Multiplier, nullable: true);
        }
    }
}
