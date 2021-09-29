using GraphQL.Types;
using Obras.Business.PeopleDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.GraphQLModels.PeopleDomain.Enums;
using Obras.GraphQLModels.SharedDomain.Enums;

namespace Obras.GraphQLModels.PeopleDomain.InputTypes
{
    public class PeopleByInputType : InputObjectGraphType<SortingDetails<PeopleSortingFields>>
    {
        public PeopleByInputType()
        {
            Field<PeopleSortingFieldsEnumType>("field", resolve: context => context.Source.Field);
            Field<SortingDirectionEnumType>("direction", resolve: context => context.Source.Direction);
        }
    }
}
