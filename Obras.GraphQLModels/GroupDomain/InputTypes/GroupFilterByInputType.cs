using GraphQL.Types;
using Obras.Business.GroupDomain.Models;

namespace Obras.GraphQLModels.GroupDomain.InputTypes
{
    public class GroupFilterByInputType : InputObjectGraphType<GroupFilter>
    {
        public GroupFilterByInputType()
        {
            Field(x => x.Id, nullable: true);
            Field(x => x.Description, nullable: true);
            Field(x => x.CompanyId, nullable: true);
            Field(x => x.Active, nullable: true);
        }
    }
}
