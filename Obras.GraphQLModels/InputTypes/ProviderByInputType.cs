namespace Obras.GraphQLModels.InputTypes
{
    using GraphQL.Types;
    using Obras.Business.Enums;
    using Obras.Business.Models;
    using Obras.GraphQLModels.Enums;

    public class ProviderByInputType : InputObjectGraphType<SortingDetails<ProviderSortingFields>>
    {
        public ProviderByInputType()
        {
            Field<ProviderSortingFieldsEnumType>("field", resolve: context => context.Source.Field);
            Field<SortingDirectionEnumType>("direction", resolve: context => context.Source.Direction);
        }
    }
}
