namespace Obras.GraphQLModels.SharedDomain.Mutations
{
    using GraphQL.Types;
    using Obras.GraphQLModels.BrandDomain.Mutations;
    using Obras.GraphQLModels.CompanyDomain.Mutations;
    using Obras.GraphQLModels.ConstructionBatchDomain.Mutations;
    using Obras.GraphQLModels.ConstructionDomain.Mutations;
    using Obras.GraphQLModels.ConstructionInvestorDomain.Mutations;
    using Obras.GraphQLModels.DocumentationDomain.Mutations;
    using Obras.GraphQLModels.EmployeeDomain.Mutations;
    using Obras.GraphQLModels.ExpenseDomain.Mutations;
    using Obras.GraphQLModels.OutsourcedDomain.Mutations;
    using Obras.GraphQLModels.PeopleDomain.Mutations;
    using Obras.GraphQLModels.ProductDomain.Mutations;
    using Obras.GraphQLModels.ProductProviderDomain.Mutations;
    using Obras.GraphQLModels.ProviderDomain.Mutations;
    using Obras.GraphQLModels.ResponsibilityDomain.Mutations;

    public class ObrasMutation : ObjectGraphType
    {
        public ObrasMutation()
        {
            Name = nameof(ObrasMutation);
            Field<CompanyMutation>("companies", resolve: context => new { });
            Field<ProductMutation>("products", resolve: context => new { });
            Field<ProviderMutation>("providers", resolve: context => new { });
            Field<BrandMutation>("brands", resolve: context => new { });
            Field<DocumentationMutation>("documentations", resolve: context => new { });
            Field<ResponsibilityMutation>("responsibilities", resolve: context => new { });
            Field<ExpenseMutation>("expenses", resolve: context => new { });
            Field<PeopleMutation>("peoples", resolve: context => new { });
            Field<EmployeeMutation>("employees", resolve: context => new { });
            Field<OutsourcedMutation>("outsourceds", resolve: context => new { });
            Field<ProductProviderMutation>("productProviders", resolve: context => new { });
            Field<ConstructionMutation>("constructions", resolve: context => new { });
            Field<ConstructionInvestorMutation>("constructionInvestors", resolve: context => new { });
            Field<ConstructionBatchMutation>("constructionBatchs", resolve: context => new { });
        }
    }
}
