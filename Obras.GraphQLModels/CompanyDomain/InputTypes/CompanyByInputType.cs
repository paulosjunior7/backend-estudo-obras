namespace Obras.GraphQLModels.CompanyDomain.InputTypes
{
    using GraphQL.Types;
    using Obras.Business.CompanyDomain.Enums;
    using Obras.Business.SharedDomain.Models;
    using Obras.GraphQLModels.CompanyDomain.Enums;
    using Obras.GraphQLModels.SharedDomain.Enums;

    public class CompanyByInputType : InputObjectGraphType<SortingDetails<CompanySortingFields>>
    {
        public CompanyByInputType()
        {
            Field<CompanySortingFieldsEnumType>("field", resolve: context => context.Source.Field);
            Field<SortingDirectionEnumType>("direction", resolve: context => context.Source.Direction);
        }
    }
}
