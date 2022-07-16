using GraphQL.Types;
using Obras.Business.OutsoursedDomain.Enums;

namespace Obras.GraphQLModels.OutsourcedDomain.Enums
{
    public class OutsourcedSortingFieldsEnumType : EnumerationGraphType<OutsourcedSortingFields>
    {
        public OutsourcedSortingFieldsEnumType()
        {
            Name = nameof(OutsourcedSortingFieldsEnumType);
        }
    }
}
