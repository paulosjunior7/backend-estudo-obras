using GraphQL.Types;
using Obras.Business.ConstructionBatchDomain.Enums;

namespace Obras.GraphQLModels.ConstructionBatchDomain.Enums
{
    public class ConstructionBatchSortingFieldsEnumType : EnumerationGraphType<ConstructionBatchSortingFields>
    {
        public ConstructionBatchSortingFieldsEnumType()
        {
            Name = nameof(ConstructionBatchSortingFieldsEnumType);
        }
    }
}
