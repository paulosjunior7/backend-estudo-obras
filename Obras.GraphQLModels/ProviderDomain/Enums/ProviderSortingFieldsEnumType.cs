namespace Obras.GraphQLModels.ProviderDomain.Enums
{
    using GraphQL.Types;
    using Obras.Business.ProviderDomain.Enums;

    public class ProviderSortingFieldsEnumType : EnumerationGraphType<ProviderSortingFields>
    {
        public ProviderSortingFieldsEnumType()
        {
            Name = nameof(ProviderSortingFieldsEnumType);
        }
    }
}
