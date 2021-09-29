using GraphQL;
using GraphQL.Types;
using GraphQL.Types.Relay.DataObjects;
using Obras.Business.PeopleDomain.Enums;
using Obras.Business.PeopleDomain.Models;
using Obras.Business.PeopleDomain.Services;
using Obras.Business.SharedDomain.Helpers;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;
using Obras.GraphQLModels.PeopleDomain.InputTypes;
using Obras.GraphQLModels.PeopleDomain.Types;
using Obras.GraphQLModels.SharedDomain.InputTypes;
using System.Linq;

namespace Obras.GraphQLModels.PeopleDomainQueries
{
    public class PeopleQuery : ObjectGraphType
    {
        public PeopleQuery(IPeopleService peopleService, ObrasDBContext dBContext)
        {
            Connection<PeopleType>()
                .Name("findall")
                .Unidirectional()
                .AuthorizeWith("LoggedIn")
                .Argument<PaginationDetailsType>("pagination", "Paginarion")
                .Argument<PeopleByInputType>("sort", "Pass field & direction on which you want to sort data")
                .Argument<PeopleFilterByInputType>("filter", "filter on which you want to sort data")
                .ResolveAsync(async context =>
                {
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);
                    var pageRequest = new PageRequest<PeopleFilter, PeopleSortingFields>
                    {
                        Pagination = context.GetArgument<PaginationDetails>("pagination") ?? new PaginationDetails(),
                        Filter = context.GetArgument<PeopleFilter>("filter") ?? new PeopleFilter(),
                        OrderBy = context.GetArgument<SortingDetails<PeopleSortingFields>>("sort")
                    };

                    pageRequest.Filter.CompanyId = (int)(pageRequest.Filter.CompanyId == null ? user.CompanyId : pageRequest.Filter.CompanyId);

                    var pageResponse = await peopleService.GetPeoplesAsync(pageRequest);

                    (string startCursor, string endCursor) = CursorHelper.GetFirstAndLastCursor(pageResponse.Nodes.Select(x => x.Id));

                    var edge = pageResponse.Nodes.Select(x => new Edge<People>
                    {
                        Cursor = CursorHelper.ToCursor(x.Id),
                        Node = x
                    }).ToList();

                    var connection = new Connection<People>()
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
