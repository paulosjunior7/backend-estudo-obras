namespace Obras.GraphQLModels.SharedDomain.InputTypes
{
    using GraphQL.Types;
    using Obras.Business.SharedDomain.Models;

    public class PaginationDetailsType : InputObjectGraphType<PaginationDetails>
    {
        public PaginationDetailsType()
        {
            Field(x => x.PageSize, nullable: true);
            Field(x => x.PageNumber, nullable: true);
        }
    }
}
