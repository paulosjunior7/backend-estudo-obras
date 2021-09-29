using GraphQL.Types;
using Obras.Business.BrandDomain.Enums;

namespace Obras.GraphQLModels.BrandDomain.Enums
{
    public class BrandSortingFieldsEnumType : EnumerationGraphType<BrandSortingFields>
    {
        public BrandSortingFieldsEnumType()
        {
            Name = nameof(BrandSortingFieldsEnumType);
        }
    }
}
