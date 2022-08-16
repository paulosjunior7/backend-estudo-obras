using GraphQL.Types;
using Obras.Business.GroupDomain.Models;

namespace Obras.GraphQLModels.GroupDomain.InputTypes
{
    public class GroupInputType : InputObjectGraphType<GroupModel>
    {
        public GroupInputType()
        {
            Name = nameof(GroupInputType);

            Field(x => x.Description, nullable: true);
            Field(x => x.Active);
        }
    }
}
