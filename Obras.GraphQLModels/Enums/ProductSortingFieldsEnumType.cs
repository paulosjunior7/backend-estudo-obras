namespace Obras.GraphQLModels.Enums
{
    using GraphQL.Types;
    using Obras.Business.Enums;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ProductSortingFieldsEnumType : EnumerationGraphType<ProductSortingFields>
    {
        public ProductSortingFieldsEnumType()
        {
            Name = nameof(ProductSortingFieldsEnumType);
        }
    }
}
