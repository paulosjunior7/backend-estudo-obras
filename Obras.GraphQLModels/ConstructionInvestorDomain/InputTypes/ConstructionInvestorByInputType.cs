using GraphQL.Types;
using Obras.Business.ConstructionDomain.Enums;
using Obras.Business.ConstructionInvestorDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.GraphQLModels.ConstructionInvestorDomain.Enums;
using Obras.GraphQLModels.SharedDomain.Enums;

namespace Obras.GraphQLModels.ConstructionInvestorDomain.InputTypes
{
    public class ConstructionInvestorByInputType : InputObjectGraphType<SortingDetails<ConstructionInvestorSortingFields>>
    {
        public ConstructionInvestorByInputType()
        {
            Field<ConstructionInvestorSortingFieldsEnumType>("field", resolve: context => context.Source.Field);
            Field<SortingDirectionEnumType>("direction", resolve: context => context.Source.Direction);
        }
    }
}
