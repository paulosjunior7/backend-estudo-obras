namespace Obras.GraphQLModels.Queries
{
    using GraphQL;
    using GraphQL.Types;
    using GraphQL.Types.Relay.DataObjects;
    using Obras.Business.Enums;
    using Obras.Business.Helpers;
    using Obras.Business.Models;
    using Obras.Business.Services;
    using Obras.Data;
    using Obras.Data.Entities;
    using Obras.GraphQLModels.InputTypes;
    using Obras.GraphQLModels.Types;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ProductQuery : ObjectGraphType
    {
        public ProductQuery(IProductService productService, ObrasDBContext dBContext)
        {
            Connection<ProductType>()
                .Name("findall")
                .Unidirectional()
                .AuthorizeWith("LoggedIn")
                .Argument<PaginationDetailsType>("pagination", "Paginarion")
                .Argument<ProductByInputType>("sort", "Pass field & direction on which you want to sort data")
                .Argument<ProductFilterByInputType>("filter", "filter on which you want to sort data")
                .ResolveAsync(async context =>
                {
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);
                    var pageRequest = new PageRequest<ProductFilter, ProductSortingFields>
                    {
                        Pagination = context.GetArgument<PaginationDetails>("pagination") ?? new PaginationDetails(),
                        Filter = context.GetArgument<ProductFilter>("filter") ?? new ProductFilter(),
                        OrderBy = context.GetArgument<SortingDetails<ProductSortingFields>>("sort")
                    };

                    pageRequest.Filter.CompanyId = (int)(pageRequest.Filter.CompanyId == null ? user.CompanyId : pageRequest.Filter.CompanyId);

                    var pageResponse = await productService.GetProductsAsync(pageRequest);

                    (string startCursor, string endCursor) = CursorHelper.GetFirstAndLastCursor(pageResponse.Nodes.Select(x => x.Id));

                    var edge = pageResponse.Nodes.Select(x => new Edge<Product>
                    {
                        Cursor = CursorHelper.ToCursor(x.Id),
                        Node = x
                    }).ToList();

                    var connection = new Connection<Product>()
                    {
                        Edges = edge,
                        TotalCount = pageResponse.TotalCount,
                        PageInfo = new PageInfo
                        {
                            HasNextPage = pageResponse.HasNextPage,
                            HasPreviousPage = pageResponse.HasPreviousPage,
                            StartCursor = startCursor,
                            EndCursor = endCursor
                        }
                    };

                    return connection;
                });
        }
    }
}
