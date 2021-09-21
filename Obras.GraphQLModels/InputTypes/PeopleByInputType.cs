using GraphQL.Types;
using Obras.Business.Enums;
using Obras.Business.Models;
using Obras.GraphQLModels.Enums;

namespace Obras.GraphQLModels.InputTypes
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
