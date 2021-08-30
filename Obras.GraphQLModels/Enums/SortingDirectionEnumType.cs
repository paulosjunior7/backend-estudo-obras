namespace Obras.GraphQLModels.Enums
{
    using GraphQL.Types;
    using Obras.Business.Enums;

    public class SortingDirectionEnumType : EnumerationGraphType<SortingDirection>
    {
        public SortingDirectionEnumType()
        {
            Name = nameof(SortingDirectionEnumType);
        }
    }
}
