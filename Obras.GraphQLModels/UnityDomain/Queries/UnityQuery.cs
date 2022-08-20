using GraphQL;
using GraphQL.Types;
using GraphQL.Types.Relay.DataObjects;
using Obras.Business.SharedDomain.Helpers;
using Obras.Business.SharedDomain.Models;
using Obras.Business.UnitDomain.Enums;
using Obras.Business.UnitDomain.Models;
using Obras.Business.UnitDomain.Services;
using Obras.Data;
using Obras.Data.Entities;
using Obras.GraphQLModels.SharedDomain.InputTypes;
using Obras.GraphQLModels.UnityDomain.InputTypes;
using Obras.GraphQLModels.UnityDomain.Types;
using System.Linq;

namespace Obras.GraphQLModels.UnityDomain.Queries
{
    public class UnityQuery : ObjectGraphType
    {
        public UnityQuery(IUnityService service, ObrasDBContext dBContext)
        {
            Connection<UnityType>()
                .Name("findall")
                .Unidirectional()
                .AuthorizeWith("LoggedIn")
                .Argument<PaginationDetailsType>("pagination", "Paginarion")
                .Argument<UnityByInputType>("sort", "Pass field & direction on which you want to sort data")
                .Argument<UnityFilterByInputType>("filter", "filter on which you want to sort data")
                .ResolveAsync(async context =>
                {
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);
                    var pageRequest = new PageRequest<UnityFilter, UnitySortingFields>
                    {
                        Pagination = context.GetArgument<PaginationDetails>("pagination") ?? new PaginationDetails(),
                        Filter = context.GetArgument<UnityFilter>("filter") ?? new UnityFilter(),
                        OrderBy = context.GetArgument<SortingDetails<UnitySortingFields>>("sort")
                    };

                    pageRequest.Filter.CompanyId = (int)(pageRequest.Filter.CompanyId == null ? user.CompanyId : pageRequest.Filter.CompanyId);

                    var pageResponse = await service.GetAsync(pageRequest);

                    (string startCursor, string endCursor) = CursorHelper.GetFirstAndLastCursor(pageResponse.Nodes.Select(x => x.Id));

                    var edge = pageResponse.Nodes.Select(x => new Edge<Unity>
                    {
                        Cursor = CursorHelper.ToCursor(x.Id),
                        Node = x
                    }).ToList();

                    var connection = new Connection<Unity>()
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

            FieldAsync<UnityType>(
            name: "findById",
            arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }),
            resolve: async context =>
            {
                var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                var user = await dBContext.User.FindAsync(userId);

                var pageResponse = await service.GetId(context.GetArgument<int>("id"));

                return pageResponse;
            });
        }
    }
}
