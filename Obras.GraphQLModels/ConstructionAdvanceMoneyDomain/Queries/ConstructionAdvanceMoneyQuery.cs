using GraphQL;
using GraphQL.Types;
using GraphQL.Types.Relay.DataObjects;
using Obras.Business.ConstructionAdvanceMoneyDomain.Enums;
using Obras.Business.ConstructionAdvanceMoneyDomain.Models;
using Obras.Business.ConstructionAdvanceMoneyDomain.Services;
using Obras.Business.SharedDomain.Helpers;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;
using Obras.GraphQLModels.ConstructionAdvanceMoneyDomain.InputTypes;
using Obras.GraphQLModels.ConstructionAdvanceMoneyDomain.Types;
using Obras.GraphQLModels.SharedDomain.InputTypes;
using System.Linq;

namespace Obras.GraphQLModels.ConstructionAdvanceMoneyDomain.Queries
{ 
    public class ConstructionAdvanceMoneyQuery : ObjectGraphType
    {
        public ConstructionAdvanceMoneyQuery(IConstructionAdvanceMoneyService service, ObrasDBContext dBContext)
        {
            Connection<ConstructionAdvanceMoneyType>()
                .Name("findall")
                .Unidirectional()
                .AuthorizeWith("LoggedIn")
                .Argument<PaginationDetailsType>("pagination", "Paginarion")
                .Argument<ConstructionAdvanceMoneyByInputType>("sort", "Pass field & direction on which you want to sort data")
                .Argument<ConstructionAdvanceMoneyFilterByInputType>("filter", "filter on which you want to sort data")
                .ResolveAsync(async context =>
                {
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);
                    var pageRequest = new PageRequest<ConstructionAdvanceMoneyFilter, ConstructionAdvanceMoneySortingFields>
                    {
                        Pagination = context.GetArgument<PaginationDetails>("pagination") ?? new PaginationDetails(),
                        Filter = context.GetArgument<ConstructionAdvanceMoneyFilter>("filter") ?? new ConstructionAdvanceMoneyFilter(),
                        OrderBy = context.GetArgument<SortingDetails<ConstructionAdvanceMoneySortingFields>>("sort")
                    };

                    var pageResponse = await service.GetAsync(pageRequest);

                    (string startCursor, string endCursor) = CursorHelper.GetFirstAndLastCursor(pageResponse.Nodes.Select(x => x.Id));

                    var edge = pageResponse.Nodes.Select(x => new Edge<ConstructionAdvanceMoney>
                    {
                        Cursor = CursorHelper.ToCursor(x.Id),
                        Node = x
                    }).ToList();

                    var connection = new Connection<ConstructionAdvanceMoney>()
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

            FieldAsync<ConstructionAdvanceMoneyType>(
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
