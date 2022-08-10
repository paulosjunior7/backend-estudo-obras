namespace Obras.GraphQLModels.ConstructionDomain.Enums
{
    using GraphQL.Types;
    using Obras.Business.ConstructionDomain.Enums;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ConstructionSortingFieldsEnumType : EnumerationGraphType<ConstructionSortingFields>
    {
        public ConstructionSortingFieldsEnumType()
        {
            Name = nameof(ConstructionSortingFieldsEnumType);
        }
    }
}
