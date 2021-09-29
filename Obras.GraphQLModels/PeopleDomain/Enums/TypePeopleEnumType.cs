using GraphQL.Types;
using Obras.Data.Enums;

namespace Obras.GraphQLModels.PeopleDomain.Enums
{
    public class TypePeopleEnumType : EnumerationGraphType<TypePeople>
    {
        public TypePeopleEnumType()
        {
            Name = nameof(TypePeopleEnumType);
        }
    }
}
