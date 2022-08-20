using GraphQL;
using GraphQL.Types;
using GraphQL.Types.Relay.DataObjects;
using Obras.Business.GroupDomain.Enums;
using Obras.Business.GroupDomain.Models;
using Obras.Business.GroupDomain.Services;
using Obras.Business.SharedDomain.Helpers;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;
using Obras.GraphQLModels.GroupDomain.InputTypes;
using Obras.GraphQLModels.GroupDomain.Types;
using Obras.GraphQLModels.SharedDomain.InputTypes;
using System.Linq;

namespace Obras.GraphQLModels.GroupDomain.Queries
{
    public class GroupQuery : ObjectGraphType
    {
        public GroupQuery(IGroupService service, ObrasDBContext dBContext)
        {
            Connection<GroupType>()
                .Name("findall")
                .Unidirectional()
                .AuthorizeWith("LoggedIn")
                .Argument<PaginationDetailsType>("pagination", "Paginarion")
                .Argument<GroupByInputType>("sort", "Pass field & direction on which you want to sort data")
                .Argument<GroupFilterByInputType>("filter", "filter on which you want to sort data")
                .ResolveAsync(async context =>
                {
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);
                    var pageRequest = new PageRequest<GroupFilter, GroupSortingFields>
                    {
                        Pagination = context.GetArgument<PaginationDetails>("pagination") ?? new PaginationDetails(),
                        Filter = context.GetArgument<GroupFilter>("filter") ?? new GroupFilter(),
                        OrderBy = context.GetArgument<SortingDetails<GroupSortingFields>>("sort")
                    };

                    pageRequest.Filter.CompanyId = (int)(pageRequest.Filter.CompanyId == null ? user.CompanyId : pageRequest.Filter.CompanyId);

                    var pageResponse = await service.GetAsync(pageRequest);

                    (string startCursor, string endCursor) = CursorHelper.GetFirstAndLastCursor(pageResponse.Nodes.Select(x => x.Id));

                    var edge = pageResponse.Nodes.Select(x => new Edge<Group>
                    {
                        Cursor = CursorHelper.ToCursor(x.Id),
                        Node = x
                    }).ToList();

                    var connection = new Connection<Group>()
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

            FieldAsync<GroupType>(
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
