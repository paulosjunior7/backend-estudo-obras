namespace Obras.GraphQLModels.ProviderDomain.Queries
{
    using GraphQL;
    using GraphQL.Types;
    using GraphQL.Types.Relay.DataObjects;
    using Obras.Business.ProviderDomain.Enums;
    using Obras.Business.ProviderDomain.Models;
    using Obras.Business.ProviderDomain.Services;
    using Obras.Business.SharedDomain.Helpers;
    using Obras.Business.SharedDomain.Models;
    using Obras.Data;
    using Obras.Data.Entities;
    using Obras.GraphQLModels.ProviderDomain.InputTypes;
    using Obras.GraphQLModels.ProviderDomain.Types;
    using Obras.GraphQLModels.SharedDomain.InputTypes;
    using System.Linq;

    public class ProviderQuery : ObjectGraphType
    {
        public ProviderQuery(IProviderService providerService, ObrasDBContext dBContext)
        {
            Connection<ProviderType>()
                .Name("findall")
                .Unidirectional()
                .AuthorizeWith("LoggedIn")
                .Argument<PaginationDetailsType>("pagination", "Paginarion")
                .Argument<ProviderByInputType>("sort", "Pass field & direction on which you want to sort data")
                .Argument<ProviderFilterByInputType>("filter", "filter on which you want to sort data")
                .ResolveAsync(async context =>
                {
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);

                    var pageRequest = new PageRequest<ProviderFilter, ProviderSortingFields>
                    {
                        Pagination = context.GetArgument<PaginationDetails>("pagination") ?? new PaginationDetails(),
                        Filter = context.GetArgument<ProviderFilter>("filter") ?? new ProviderFilter(),
                        OrderBy = context.GetArgument<SortingDetails<ProviderSortingFields>>("sort") 
                    };

                    pageRequest.Filter.CompanyId = (int)(pageRequest.Filter.CompanyId == null ? user.CompanyId : pageRequest.Filter.CompanyId);

                    var pageResponse = await providerService.GetProvidersAsync(pageRequest);

                    (string startCursor, string endCursor) = CursorHelper.GetFirstAndLastCursor(pageResponse.Nodes.Select(x => x.Id));

                    var edge = pageResponse.Nodes.Select(x => new Edge<Provider>
                    {
                        Cursor = CursorHelper.ToCursor(x.Id),
                        Node = x
                    }).ToList();

                    var connection = new Connection<Provider>()
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

            FieldAsync<ProviderType>(
            name: "findById",
            arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }),
            resolve: async context =>
            {
                var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                var user = await dBContext.User.FindAsync(userId);

                var pageResponse = await providerService.GetProviderId(context.GetArgument<int>("id"));

                return pageResponse;
            });
        }
    }
}
