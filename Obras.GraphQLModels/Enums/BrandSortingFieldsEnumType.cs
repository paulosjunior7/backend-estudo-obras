using GraphQL.Types;
using Obras.Business.Enums;

namespace Obras.GraphQLModels.Enums
{
    public class BrandSortingFieldsEnumType : EnumerationGraphType<BrandSortingFields>
    {
        public BrandSortingFieldsEnumType()
        {
            Name = nameof(BrandSortingFieldsEnumType);
        }
    }
}
