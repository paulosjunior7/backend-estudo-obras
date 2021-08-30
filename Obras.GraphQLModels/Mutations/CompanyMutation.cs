namespace Obras.GraphQLModels.Mutations
{
    using GraphQL;
    using GraphQL.Types;
    using Obras.Business.Models;
    using Obras.Business.Services;
    using Obras.GraphQLModels.InputTypes;
    using Obras.GraphQLModels.Types;

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

                    return await companyService.UpdateStatusAsync(companyId, companyModel);
                });
        }
    }
}
