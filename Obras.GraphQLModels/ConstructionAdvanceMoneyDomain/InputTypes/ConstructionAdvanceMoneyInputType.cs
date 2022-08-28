using GraphQL.Types;
using Obras.Business.ConstructionAdvanceMoneyDomain.Models;

namespace Obras.GraphQLModels.ConstructionAdvanceMoneyDomain.InputTypes
{
    public class ConstructionAdvanceMoneyInputType : InputObjectGraphType<ConstructionAdvanceMoneyModel>
    {
        public ConstructionAdvanceMoneyInputType()
        {
            Name = nameof(ConstructionAdvanceMoneyInputType);

            Field(x => x.ConstructionInvestorId);
            Field(x => x.ConstructionId);
            Field(x => x.Date);
            Field(x => x.Value);
            Field(x => x.Active);
        }
    }
}
