namespace Obras.GraphQLModels.Enums
{
    using GraphQL.Types;
    using Obras.Business.Enums;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ProviderSortingFieldsEnumType : EnumerationGraphType<ProviderSortingFields>
    {
        public ProviderSortingFieldsEnumType()
        {
            Name = nameof(ProviderSortingFieldsEnumType);
        }
    }
}
