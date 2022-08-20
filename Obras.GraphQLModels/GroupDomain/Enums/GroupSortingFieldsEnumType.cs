using GraphQL.Types;
using Obras.Business.GroupDomain.Enums;

namespace Obras.GraphQLModels.GroupDomain.Enums
{
    public class GroupSortingFieldsEnumType : EnumerationGraphType<GroupSortingFields>
    {
        public GroupSortingFieldsEnumType()
        {
            Name = nameof(GroupSortingFieldsEnumType);
        }
    }
}
