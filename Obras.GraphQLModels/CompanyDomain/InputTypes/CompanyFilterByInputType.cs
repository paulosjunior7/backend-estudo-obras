namespace Obras.GraphQLModels.CompanyDomain.InputTypes
{
    using GraphQL.Types;
    using Obras.Business.CompanyDomain.Models;

    public class CompanyFilterByInputType : InputObjectGraphType<CompanyFilter>
    {
        public CompanyFilterByInputType()
        {
            Field(x => x.Id, nullable: true);
            Field(x => x.Cnpj, nullable: true);
            Field(x => x.CorporateName, nullable: true);
            Field(x => x.Active, nullable: true);
            Field(x => x.City, nullable: true);
            Field(x => x.FantasyName, nullable: true);
            Field(x => x.Neighbourhood, nullable: true);
            Field(x => x.State, nullable: true);
        }
    }
}
