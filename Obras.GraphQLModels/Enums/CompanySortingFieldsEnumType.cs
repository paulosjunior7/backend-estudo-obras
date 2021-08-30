namespace Obras.GraphQLModels.Enums
{
    using GraphQL.Types;
    using Obras.Business.Enums;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CompanySortingFieldsEnumType : EnumerationGraphType<CompanySortingFields>
    {
        public CompanySortingFieldsEnumType()
        {
            Name = nameof(CompanySortingFieldsEnumType);
        }
    }
}
