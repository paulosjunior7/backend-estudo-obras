using GraphQL;
using GraphQL.Types;
using GraphQL.Types.Relay.DataObjects;
using Obras.Business.ProductProviderDomain.Enums;
using Obras.Business.ProductProviderDomain.Models;
using Obras.Business.ProductProviderDomain.Services;
using Obras.Business.SharedDomain.Helpers;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;
using Obras.GraphQLModels.ProductProviderDomain.InputTypes;
using Obras.GraphQLModels.ProductProviderDomain.Types;
using Obras.GraphQLModels.SharedDomain.InputTypes;
using System.Linq;

namespace Obras.GraphQLModels.ProductProviderDomain.Queries
{
    public class ProductProviderQuery : ObjectGraphType
    {
        public ProductProviderQuery(IProductProviderService productProviderService, ObrasDBContext dBContext)
        {
            Connection<ProductProviderType>()
                .Name("findall")
                .Unidirectional()
                .AuthorizeWith("LoggedIn")
                .Argument<PaginationDetailsType>("pagination", "Paginarion")
                .Argument<ProductProviderByInputType>("sort", "Pass field & direction on which you want to sort data")
                .Argument<ProductProviderFilterByInputType>("filter", "filter on which you want to sort data")
                .ResolveAsync(async context =>
                {
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);
                    var pageRequest = new PageRequest<ProductProviderFilter, ProductProviderSortingFields>
                    {
                        Pagination = context.GetArgument<PaginationDetails>("pagination") ?? new PaginationDetails(),
                        Filter = context.GetArgument<ProductProviderFilter>("filter") ?? new ProductProviderFilter(),
                        OrderBy = context.GetArgument<SortingDetails<ProductProviderSortingFields>>("sort")
                    };

                    var pageResponse = await productProviderService.GetProductProvidersAsync(pageRequest);

                    (string startCursor, string endCursor) = CursorHelper.GetFirstAndLastCursor(pageResponse.Nodes.Select(x => x.Id));

                    var edge = pageResponse.Nodes.Select(x => new Edge<ProductProvider>
                    {
                        Cursor = CursorHelper.ToCursor(x.Id),
                        Node = x
                    }).ToList();

                    var connection = new Connection<ProductProvider>()
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
