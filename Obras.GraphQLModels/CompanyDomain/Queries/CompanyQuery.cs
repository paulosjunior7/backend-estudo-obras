namespace Obras.GraphQLModels.CompanyDomain.Queries
{
    using GraphQL;
    using GraphQL.Types;
    using GraphQL.Types.Relay.DataObjects;
    using Obras.Business.CompanyDomain.Enums;
    using Obras.Business.CompanyDomain.Models;
    using Obras.Business.CompanyDomain.Services;
    using Obras.Business.SharedDomain.Helpers;
    using Obras.Business.SharedDomain.Models;
    using Obras.Data.Entities;
    using Obras.GraphQLModels.CompanyDomain.InputTypes;
    using Obras.GraphQLModels.CompanyDomain.Types;
    using Obras.GraphQLModels.SharedDomain.InputTypes;
    using System.Linq;

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

            FieldAsync<CompanyType>(
            name: "findById",
            arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }),
            resolve: async context =>
            {
                var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                var pageResponse = await companyService.GetCompanyId(context.GetArgument<int>("id"));

                return pageResponse;
            });
        }
    }
}
