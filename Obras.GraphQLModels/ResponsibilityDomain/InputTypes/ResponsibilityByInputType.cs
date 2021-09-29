using GraphQL.Types;
using Obras.Business.ResponsibilityDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.GraphQLModels.ResponsibilityDomain.Enums;
using Obras.GraphQLModels.SharedDomain.Enums;

namespace Obras.GraphQLModels.ResponsibilityDomain.InputTypes
{
    public class ResponsibilityByInputType : InputObjectGraphType<SortingDetails<ResponsibilitySortingFields>>
    {
        public ResponsibilityByInputType()
        {
            Field<ResponsibilitySortingFieldsEnumType>("field", resolve: context => context.Source.Field);
            Field<SortingDirectionEnumType>("direction", resolve: context => context.Source.Direction);
        }
    }
}
