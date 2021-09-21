using GraphQL.Types;
using Obras.Business.Enums;

namespace Obras.GraphQLModels.Enums
{
    public class PeopleSortingFieldsEnumType : EnumerationGraphType<PeopleSortingFields>
    {
        public PeopleSortingFieldsEnumType()
        {
            Name = nameof(PeopleSortingFieldsEnumType);
        }
    }
}
