namespace Obras.GraphQLModels.Enums
{
    using GraphQL.Types;
    using Obras.Business.Enums;

    public class CompanySortingFieldsEnumType : EnumerationGraphType<CompanySortingFields>
    {
        public CompanySortingFieldsEnumType()
        {
            Name = nameof(CompanySortingFieldsEnumType);
        }
    }
}
