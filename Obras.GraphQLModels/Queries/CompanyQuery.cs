namespace Obras.GraphQLModels.Queries
{
    using GraphQL;
    using GraphQL.Types;
    using GraphQL.Types.Relay.DataObjects;
    using Obras.Business.Enums;
    using Obras.Business.Helpers;
    using Obras.Business.Models;
    using Obras.Business.Services;
    using Obras.Data.Entities;
    using Obras.GraphQLModels.InputTypes;
    using Obras.GraphQLModels.Types;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class CompanyQuery : ObjectGraphType
    {
        public CompanyQuery(ICompanyService companyService)
        {
            Connection<CompanyType>()
                .Name("findall")
                .Unidirectional()
                .Argument<PaginationDetailsType>("pagination", "Paginarion")
                .Argument<CompanyByInputType>("sort", "Pass field & direction on which you want to sort data")
                .Argument<CompanyFilterByInputType>("filter", "filter on which you want to sort data")
                .ResolveAsync(async context =>
                {
                    var pageRequest = new PageRequest<CompanyFilter, CompanySortingFields>
                    {
                        Pagination = context.GetArgument<PaginationDetails>("pagination") ?? new PaginationDetails(),
                        Filter = context.GetArgument<CompanyFilter>("filter") ?? new CompanyFilter(),
                        OrderBy = context.GetArgument<SortingDetails<CompanySortingFields>>("sort")
                    };

                    var pageResponse = await companyService.GetCompaniesAsync(pageRequest);

                    (string startCursor, string endCursor) = CursorHelper.GetFirstAndLastCursor(pageResponse.Nodes.Select(x => x.Id));

                    var edge = pageResponse.Nodes.Select(x => new Edge<Company>
                    {
                        Cursor = CursorHelper.ToCursor(x.Id),
                        Node = x
                    }).ToList();

                    var connection = new Connection<Company>()
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
