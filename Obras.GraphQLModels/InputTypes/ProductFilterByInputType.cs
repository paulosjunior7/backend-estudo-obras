namespace Obras.GraphQLModels.InputTypes
{
    using GraphQL.Types;
    using Obras.Business.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ProductFilterByInputType : InputObjectGraphType<ProductFilter>
    {
        public ProductFilterByInputType()
        {
            Field(x => x.Id, nullable: true);
            Field(x => x.Detail, nullable: true);
            Field(x => x.Description, nullable: true);
            Field(x => x.CompanyId, nullable: true);
        }
    }
}
