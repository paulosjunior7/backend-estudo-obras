namespace Obras.GraphQLModels.ConstructionMaterialDomain.Enums
{
    using GraphQL.Types;
    using Obras.Business.ConstructionMaterialDomain.Enums;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ConstructionMaterialSortingFieldsEnumType : EnumerationGraphType<ConstructionMaterialSortingFields>
    {
        public ConstructionMaterialSortingFieldsEnumType()
        {
            Name = nameof(ConstructionMaterialSortingFieldsEnumType);
        }
    }
}
