namespace Obras.GraphQLModels.InputTypes
{
    using GraphQL.Types;
    using Obras.Business.Enums;
    using Obras.Business.Models;
    using Obras.GraphQLModels.Enums;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CompanyByInputType : InputObjectGraphType<SortingDetails<CompanySortingFields>>
    {
        public CompanyByInputType()
        {
            Field<CompanySortingFieldsEnumType>("field", resolve: context => context.Source.Field);
            Field<SortingDirectionEnumType>("direction", resolve: context => context.Source.Direction);
        }
    }
}
