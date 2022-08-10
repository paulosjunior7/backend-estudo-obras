using GraphQL.Types;
using Obras.Business.ConstructionInvestorDomain.Enums;

namespace Obras.GraphQLModels.ConstructionInvestorDomain.Enums
{
    public class ConstructionInvestorSortingFieldsEnumType : EnumerationGraphType<ConstructionInvestorSortingFields>
    {
        public ConstructionInvestorSortingFieldsEnumType()
        {
            Name = nameof(ConstructionInvestorSortingFieldsEnumType);
        }
    }
}
