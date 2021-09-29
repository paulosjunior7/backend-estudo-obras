namespace Obras.GraphQLModels.ProviderDomain.InputTypes
{
    using GraphQL.Types;
    using Obras.Business.ProviderDomain.Enums;
    using Obras.Business.SharedDomain.Models;
    using Obras.GraphQLModels.ProviderDomain.Enums;
    using Obras.GraphQLModels.SharedDomain.Enums;

    public class ProviderByInputType : InputObjectGraphType<SortingDetails<ProviderSortingFields>>
    {
        public ProviderByInputType()
        {
            Field<ProviderSortingFieldsEnumType>("field", resolve: context => context.Source.Field);
            Field<SortingDirectionEnumType>("direction", resolve: context => context.Source.Direction);
        }
    }
}
