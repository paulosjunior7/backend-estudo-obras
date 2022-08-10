using GraphQL;
using GraphQL.Types;
using GraphQL.Types.Relay.DataObjects;
using Obras.Business.ConstructionDomain.Models;
using Obras.Business.ConstructionInvestorDomain.Enums;
using Obras.Business.ConstructionInvestorDomain.Models;
using Obras.Business.ConstructionInvestorDomain.Services;
using Obras.Business.SharedDomain.Helpers;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;
using Obras.GraphQLModels.ConstructionInvestorDomain.InputTypes;
using Obras.GraphQLModels.ConstructionInvestorDomain.Types;
using Obras.GraphQLModels.SharedDomain.InputTypes;
using System.Linq;

namespace Obras.GraphQLModels.ConstructionInvestorDomain.Queries
{
    public class ConstructionInvestorQuery : ObjectGraphType
    {
        public ConstructionInvestorQuery(IConstructionInvestorService service, ObrasDBContext dBContext)
        {
            Connection<ConstructionInvestorType>()
                .Name("findall")
                .Unidirectional()
                .AuthorizeWith("LoggedIn")
                .Argument<PaginationDetailsType>("pagination", "Paginarion")
                .Argument<ConstructionInvestorByInputType>("sort", "Pass field & direction on which you want to sort data")
                .Argument<ConstructionInvestorFilterByInputType>("filter", "filter on which you want to sort data")
                .ResolveAsync(async context =>
                {
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);
                    var pageRequest = new PageRequest<ConstructionInvestorFilter, ConstructionInvestorSortingFields>
                    {
                        Pagination = context.GetArgument<PaginationDetails>("pagination") ?? new PaginationDetails(),
                        Filter = context.GetArgument<ConstructionInvestorFilter>("filter") ?? new ConstructionInvestorFilter(),
                        OrderBy = context.GetArgument<SortingDetails<ConstructionInvestorSortingFields>>("sort")
                    };

                    var pageResponse = await service.GetAsync(pageRequest);

                    (string startCursor, string endCursor) = CursorHelper.GetFirstAndLastCursor(pageResponse.Nodes.Select(x => x.Id));

                    var edge = pageResponse.Nodes.Select(x => new Edge<ConstructionInvestor>
                    {
                        Cursor = CursorHelper.ToCursor(x.Id),
                        Node = x
                    }).ToList();

                    var connection = new Connection<ConstructionInvestor>()
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

            FieldAsync<ConstructionInvestorType>(
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
