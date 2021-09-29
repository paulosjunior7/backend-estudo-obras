namespace Obras.GraphQLModels.CompanyDomain.Mutations
{
    using GraphQL;
    using GraphQL.Types;
    using Obras.Business.CompanyDomain.Models;
    using Obras.Business.CompanyDomain.Services;
    using Obras.GraphQLModels.CompanyDomain.InputTypes;
    using Obras.GraphQLModels.CompanyDomain.Types;

    public class CompanyMutation : ObjectGraphType
    {
        public CompanyMutation(ICompanyService companyService)
        {
            Name = nameof(CompanyMutation);

            FieldAsync<CompanyType>(
                name: "createCompany",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<CompanyInputType>> { Name = "company" }),
                resolve: async context =>
                {
                    var companyModel = context.GetArgument<CompanyModel>("company");

                    var company = await companyService.CreateAsync(companyModel);
                    return company;
                });

            FieldAsync<CompanyType>(
                name: "updateCompany",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<CompanyInputType>> { Name = "company" }),
                resolve: async context =>
                {
                    int companyId = context.GetArgument<int>("id");
                    var companyModel = context.GetArgument<CompanyModel>("company");

                    return await companyService.UpdateCompanyAsync(companyId, companyModel);
                });
        }
    }
}
