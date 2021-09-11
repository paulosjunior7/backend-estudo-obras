namespace Obras.GraphQLModels.Enums
{
    using GraphQL.Types;
    using Obras.Business.Enums;

    public class ProviderSortingFieldsEnumType : EnumerationGraphType<ProviderSortingFields>
    {
        public ProviderSortingFieldsEnumType()
        {
            Name = nameof(ProviderSortingFieldsEnumType);
        }
    }
}
