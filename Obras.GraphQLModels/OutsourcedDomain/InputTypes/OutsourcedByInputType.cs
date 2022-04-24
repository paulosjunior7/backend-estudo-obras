using GraphQL.Types;
using Obras.Business.OutsoursedDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.GraphQLModels.OutsourcedDomain.Enums;
using Obras.GraphQLModels.SharedDomain.Enums;

namespace Obras.GraphQLModels.OutsourcedDomain.InputTypes
{
    public class OutsourcedByInputType : InputObjectGraphType<SortingDetails<OutsourcedSortingFields>>
    {
        public OutsourcedByInputType()
        {
            Field<OutsourcedSortingFieldsEnumType>("field", resolve: context => context.Source.Field);
            Field<SortingDirectionEnumType>("direction", resolve: context => context.Source.Direction);
        }
    }
}
