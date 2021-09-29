namespace Obras.GraphQLModels.CompanyDomain.Enums
{
    using GraphQL.Types;
    using Obras.Business.CompanyDomain.Enums;

    public class CompanySortingFieldsEnumType : EnumerationGraphType<CompanySortingFields>
    {
        public CompanySortingFieldsEnumType()
        {
            Name = nameof(CompanySortingFieldsEnumType);
        }
    }
}
