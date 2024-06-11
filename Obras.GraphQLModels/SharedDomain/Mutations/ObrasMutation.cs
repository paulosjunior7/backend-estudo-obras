namespace Obras.GraphQLModels.SharedDomain.Mutations
{
    using GraphQL.Types;
    using Obras.GraphQLModels.BrandDomain.Mutations;
    using Obras.GraphQLModels.CompanyDomain.Mutations;
    using Obras.GraphQLModels.ConstructionAdvanceMoneyDomain.Mutations;
    using Obras.GraphQLModels.ConstructionBatchDomain.Mutations;
    using Obras.GraphQLModels.ConstructionDocumentationDomain.Mutations;
    using Obras.GraphQLModels.ConstructionDomain.Mutations;
    using Obras.GraphQLModels.ConstructionExpenseDomain.Mutations;
    using Obras.GraphQLModels.ConstructionHouseDomain.Mutations;
    using Obras.GraphQLModels.ConstructionInvestorDomain.Mutations;
    using Obras.GraphQLModels.ConstructionManpowerDomain.Mutations;
    using Obras.GraphQLModels.ConstructionMaterialDomain.Mutations;
    using Obras.GraphQLModels.DocumentationDomain.Mutations;
    using Obras.GraphQLModels.EmployeeDomain.Mutations;
    using Obras.GraphQLModels.ExpenseDomain.Mutations;
    using Obras.GraphQLModels.GroupDomain.Mutations;
    using Obras.GraphQLModels.OutsourcedDomain.Mutations;
    using Obras.GraphQLModels.PeopleDomain.Mutations;
    using Obras.GraphQLModels.ProductDomain.Mutations;
    using Obras.GraphQLModels.ProductProviderDomain.Mutations;
    using Obras.GraphQLModels.ProviderDomain.Mutations;
    using Obras.GraphQLModels.ResponsibilityDomain.Mutations;
    using Obras.GraphQLModels.UnityDomain.Mutations;

    public class ObrasMutation : ObjectGraphType
    {
        public ObrasMutation()
        {
            Name = nameof(ObrasMutation);
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
            Field<ConstructionHouseMutation>("constructionHouse", resolve: context => new { });
            Field<UnityMutation>("unity", resolve: context => new { });
            Field<GroupMutation>("group", resolve: context => new { });
            Field<ConstructionMaterialMutation>("constructionMaterials", resolve: context => new { });
            Field<ConstructionManpowerMutation>("constructionManpowers", resolve: context => new { });
            Field<ConstructionDocumentationMutation>("constructionDocumentations", resolve: context => new { });
            Field<ConstructionExpenseMutation>("constructionExpenses", resolve: context => new { });
            Field<ConstructionAdvanceMoneyMutation>("constructionAdvancesMoney", resolve: context => new { });
        }
    }
}
