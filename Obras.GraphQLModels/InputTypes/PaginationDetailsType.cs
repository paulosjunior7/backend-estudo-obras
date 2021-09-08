namespace Obras.GraphQLModels.InputTypes
{
    using GraphQL.Types;
    using Obras.Business.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PaginationDetailsType : InputObjectGraphType<PaginationDetails>
    {
        public PaginationDetailsType()
        {
            Field(x => x.PageSize, nullable: true);
            Field(x => x.PageNumber, nullable: true);
        }
    }
}
