using GraphQL;
using GraphQL.Types;
using GraphQL.Types.Relay.DataObjects;
using Obras.Business.BrandDomain.Enums;
using Obras.Business.BrandDomain.Models;
using Obras.Business.BrandDomain.Services;
using Obras.Business.SharedDomain.Helpers;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;
using Obras.GraphQLModels.BrandDomain.InputTypes;
using Obras.GraphQLModels.BrandDomain.Types;
using Obras.GraphQLModels.BrandDomainInputTypes;
using Obras.GraphQLModels.SharedDomain.InputTypes;
using System.Linq;

namespace Obras.GraphQLModels.BrandDomain.Queries
{
    public class BrandQuery : ObjectGraphType
    {
        public BrandQuery(IBrandService brandService, ObrasDBContext dBContext)
        {
            Connection<BrandType>()
                .Name("findall")
                .Unidirectional()
                .AuthorizeWith("LoggedIn")
                .Argument<PaginationDetailsType>("pagination", "Paginarion")
                .Argument<BrandByInputType>("sort", "Pass field & direction on which you want to sort data")
                .Argument<BrandFilterByInputType>("filter", "filter on which you want to sort data")
                .ResolveAsync(async context =>
                {
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    if (userId == null)
                    throw new ExecutionError("Verifique o token!");

                    var user = await dBContext.User.FindAsync(userId);
                    if (user == null || user.CompanyId == null)
                    throw new ExecutionError("Usuário não exite ou não possui empresa vinculada!");
                    
                    var pageRequest = new PageRequest<BrandFilter, BrandSortingFields>
                    {
                        Pagination = context.GetArgument<PaginationDetails>("pagination") ?? new PaginationDetails(),
                        Filter = context.GetArgument<BrandFilter>("filter") ?? new BrandFilter(),
                        OrderBy = context.GetArgument<SortingDetails<BrandSortingFields>>("sort")
                    };

                    pageRequest.Filter.CompanyId = (int)(pageRequest.Filter.CompanyId == null ? user.CompanyId : pageRequest.Filter.CompanyId);

                    var pageResponse = await brandService.GetBrandsAsync(pageRequest);

                    (string startCursor, string endCursor) = CursorHelper.GetFirstAndLastCursor(pageResponse.Nodes.Select(x => x.Id));

                    var edge = pageResponse.Nodes.Select(x => new Edge<Brand>
                    {
                        Cursor = CursorHelper.ToCursor(x.Id),
                        Node = x
                    }).ToList();

                    var connection = new Connection<Brand>()
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

            FieldAsync<BrandType>(
            name: "findById",
            arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }),
            resolve: async context =>
            {
                var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                var user = await dBContext.User.FindAsync(userId);

                var pageResponse = await brandService.GetBrandId(context.GetArgument<int>("id"));

                return pageResponse;
            });
        }
    }
}
