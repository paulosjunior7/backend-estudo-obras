using GraphQL;
using GraphQL.Types;
using GraphQL.Types.Relay.DataObjects;
using Obras.Business.DocumentationDomain.Enums;
using Obras.Business.DocumentationDomain.Models;
using Obras.Business.DocumentationDomain.Services;
using Obras.Business.SharedDomain.Helpers;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;
using Obras.GraphQLModels.DocumentationDomain.InputTypes;
using Obras.GraphQLModels.DocumentationDomain.Types;
using Obras.GraphQLModels.SharedDomain.InputTypes;
using System.Linq;

namespace Obras.GraphQLModels.DocumentationDomain.Queries
{
    public class DocumentationQuery : ObjectGraphType
    {
        public DocumentationQuery(IDocumentationService documentationService, ObrasDBContext dBContext)
        {
            Connection<DocumentationType>()
                .Name("findall")
                .Unidirectional()
                .AuthorizeWith("LoggedIn")
                .Argument<PaginationDetailsType>("pagination", "Paginarion")
                .Argument<DocumentationByInputType>("sort", "Pass field & direction on which you want to sort data")
                .Argument<DocumentationFilterByInputType>("filter", "filter on which you want to sort data")
                .ResolveAsync(async context =>
                {
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);
                    var pageRequest = new PageRequest<DocumentationFilter, DocumentationSortingFields>
                    {
                        Pagination = context.GetArgument<PaginationDetails>("pagination") ?? new PaginationDetails(),
                        Filter = context.GetArgument<DocumentationFilter>("filter") ?? new DocumentationFilter(),
                        OrderBy = context.GetArgument<SortingDetails<DocumentationSortingFields>>("sort")
                    };

                    pageRequest.Filter.CompanyId = (int)(pageRequest.Filter.CompanyId == null ? user.CompanyId : pageRequest.Filter.CompanyId);

                    var pageResponse = await documentationService.GetDocumentationsAsync(pageRequest);

                    (string startCursor, string endCursor) = CursorHelper.GetFirstAndLastCursor(pageResponse.Nodes.Select(x => x.Id));

                    var edge = pageResponse.Nodes.Select(x => new Edge<Documentation>
                    {
                        Cursor = CursorHelper.ToCursor(x.Id),
                        Node = x
                    }).ToList();

                    var connection = new Connection<Documentation>()
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
