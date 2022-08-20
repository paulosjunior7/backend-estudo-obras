using GraphQL.Types;
using Obras.Business.GroupDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.GraphQLModels.GroupDomain.Enums;
using Obras.GraphQLModels.SharedDomain.Enums;

namespace Obras.GraphQLModels.GroupDomain.InputTypes
{
    public class GroupByInputType : InputObjectGraphType<SortingDetails<GroupSortingFields>>
    {
        public GroupByInputType()
        {
            Field<GroupSortingFieldsEnumType>("field", resolve: context => context.Source.Field);
            Field<SortingDirectionEnumType>("direction", resolve: context => context.Source.Direction);
        }
    }
}
