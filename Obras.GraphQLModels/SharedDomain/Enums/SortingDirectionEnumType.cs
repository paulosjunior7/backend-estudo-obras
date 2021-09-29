namespace Obras.GraphQLModels.SharedDomain.Enums
{
    using GraphQL.Types;
    using Obras.Business.SharedDomain.Enums;

    public class SortingDirectionEnumType : EnumerationGraphType<SortingDirection>
    {
        public SortingDirectionEnumType()
        {
            Name = nameof(SortingDirectionEnumType);
        }
    }
}
