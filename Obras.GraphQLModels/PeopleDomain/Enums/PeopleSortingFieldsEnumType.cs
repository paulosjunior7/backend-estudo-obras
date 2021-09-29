using GraphQL.Types;
using Obras.Business.PeopleDomain.Enums;

namespace Obras.GraphQLModels.PeopleDomain.Enums
{
    public class PeopleSortingFieldsEnumType : EnumerationGraphType<PeopleSortingFields>
    {
        public PeopleSortingFieldsEnumType()
        {
            Name = nameof(PeopleSortingFieldsEnumType);
        }
    }
}
