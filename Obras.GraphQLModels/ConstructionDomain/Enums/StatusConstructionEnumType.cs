namespace Obras.GraphQLModels.ConstructionDomain.Enums
{
    using GraphQL.Types;
    using Obras.Data.Enums;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class StatusConstructionEnumType : EnumerationGraphType<StatusConstruction>
    {
        public StatusConstructionEnumType()
        {
            Name = nameof(StatusConstructionEnumType);
        }
    }
}
