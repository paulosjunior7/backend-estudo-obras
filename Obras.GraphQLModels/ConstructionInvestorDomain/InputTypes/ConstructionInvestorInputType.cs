using GraphQL.Types;
using Obras.Business.ConstructionInvestorDomain.Models;

namespace Obras.GraphQLModels.ConstructionInvestorDomain.InputTypes
{
    public class ConstructionInvestorInputType : InputObjectGraphType<ConstructionInvestorModel>
    {
        public ConstructionInvestorInputType()
        {
            Name = nameof(ConstructionInvestorInputType);

            Field(x => x.PeopleId);
            Field(x => x.ConstructionId);
            Field(x => x.Active);
        }
    }
}
