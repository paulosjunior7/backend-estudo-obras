using GraphQL.Types;
using Obras.Business.ConstructionManpowerDomain.Models;

namespace Obras.GraphQLModels.ConstructionManpowerDomain.InputTypes
{
    public class ConstructionManpowerInputType : InputObjectGraphType<ConstructionManpowerModel>
    {
        public ConstructionManpowerInputType()
        {
            Name = nameof(ConstructionManpowerInputType);

            Field(x => x.ConstructionInvestorId);
            Field(x => x.ConstructionId);
            Field(x => x.Date);
            Field(x => x.EmployeeId);
            Field(x => x.OutsourcedId);
            Field(x => x.Value);
            Field(x => x.Active);
        }
    }
}
