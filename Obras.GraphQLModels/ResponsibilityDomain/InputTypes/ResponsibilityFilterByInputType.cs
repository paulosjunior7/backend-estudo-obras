using GraphQL.Types;
using Obras.Business.ResponsibilityDomain.Models;

namespace Obras.GraphQLModels.ResponsibilityDomain.InputTypes
{
    public class ResponsibilityFilterByInputType : InputObjectGraphType<ResponsibilityFilter>
    {
        public ResponsibilityFilterByInputType()
        {
            Field(x => x.Id, nullable: true);
            Field(x => x.Description, nullable: true);
            Field(x => x.CompanyId, nullable: true);
            Field(x => x.Active, nullable: true);
        }
    }
}
