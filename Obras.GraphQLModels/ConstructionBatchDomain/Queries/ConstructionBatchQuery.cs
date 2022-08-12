using GraphQL;
using GraphQL.Types;
using GraphQL.Types.Relay.DataObjects;
using Obras.Business.ConstructionBatchDomain.Enums;
using Obras.Business.ConstructionBatchDomain.Models;
using Obras.Business.ConstructionBatchDomain.Services;
using Obras.Business.SharedDomain.Helpers;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;
using Obras.GraphQLModels.ConstructionBatchDomain.InputTypes;
using Obras.GraphQLModels.ConstructionBatchDomain.Types;
using Obras.GraphQLModels.SharedDomain.InputTypes;
using System.Linq;

namespace Obras.GraphQLModels.ConstructionBatchDomain.Queries
{
    public class ConstructionBatchQuery : ObjectGraphType
    {
        public ConstructionBatchQuery(IConstructionBatchService service, ObrasDBContext dBContext)
        {
            Connection<ConstructionBatchType>()
                .Name("findall")
                .Unidirectional()
                .AuthorizeWith("LoggedIn")
                .Argument<PaginationDetailsType>("pagination", "Paginarion")
                .Argument<ConstructionBatchByInputType>("sort", "Pass field & direction on which you want to sort data")
                .Argument<ConstructionBatchFilterByInputType>("filter", "filter on which you want to sort data")
                .ResolveAsync(async context =>
                {
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);
                    var pageRequest = new PageRequest<ConstructionBatchFilter, ConstructionBatchSortingFields>
                    {
                        Pagination = context.GetArgument<PaginationDetails>("pagination") ?? new PaginationDetails(),
                        Filter = context.GetArgument<ConstructionBatchFilter>("filter") ?? new ConstructionBatchFilter(),
                        OrderBy = context.GetArgument<SortingDetails<ConstructionBatchSortingFields>>("sort")
                    };

                    var pageResponse = await service.GetAsync(pageRequest);

                    (string startCursor, string endCursor) = CursorHelper.GetFirstAndLastCursor(pageResponse.Nodes.Select(x => x.Id));

                    var edge = pageResponse.Nodes.Select(x => new Edge<ConstructionBatch>
                    {
                        Cursor = CursorHelper.ToCursor(x.Id),
                        Node = x
                    }).ToList();

                    var connection = new Connection<ConstructionBatch>()
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

            FieldAsync<ConstructionBatchType>(
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
