using GraphQL.Types;
using Obras.Business.ConstructionInvestorDomain.Models;

namespace Obras.GraphQLModels.ConstructionInvestorDomain.InputTypes
{
    public class ConstructionInvestorFilterByInputType : InputObjectGraphType<ConstructionInvestorFilter>
    {
        public ConstructionInvestorFilterByInputType()
        {
            Field(x => x.Id, nullable: true);
            Field(x => x.ConstructionId, nullable: true);
            Field(x => x.PeopleId, nullable: true);
        }
    }
}
