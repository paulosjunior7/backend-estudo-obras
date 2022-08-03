namespace Obras.GraphQLModels.ConstructionDomain.InputTypes
{
    using GraphQL.Types;
    using Obras.Business.ConstructionDomain.Models;
    using Obras.GraphQLModels.ConstructionDomain.Enums;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ConstructionFilterByInputType : InputObjectGraphType<ConstructionFilter>
    {
        public ConstructionFilterByInputType()
        {
            Field(x => x.Id, nullable: true);
            Field(x => x.Identifier, nullable: true);
            Field(x => x.State, nullable: true);
            Field<StatusConstructionEnumType>("statusConstruction");
            Field(x => x.City, nullable: true);
            Field(x => x.CompanyId, nullable: true);
            Field(x => x.DateBegin, nullable: true);
            Field(x => x.DateEnd, nullable: true);
            Field(x => x.Active, nullable: true);
        }
    }
}
