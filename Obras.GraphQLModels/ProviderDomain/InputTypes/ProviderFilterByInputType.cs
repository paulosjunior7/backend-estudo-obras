namespace Obras.GraphQLModels.ProviderDomain.InputTypes
{
    using GraphQL.Types;
    using Obras.Business.ProviderDomain.Models;

    public class ProviderFilterByInputType : InputObjectGraphType<ProviderFilter>
    {
        public ProviderFilterByInputType()
        {
            Field(x => x.Id, nullable: true);
            Field(x => x.Cnpj, nullable: true);
            Field(x => x.Name, nullable: true);
            Field(x => x.Active, nullable: true);
            Field(x => x.City, nullable: true);
            Field(x => x.CompanyId, nullable: true);
            Field(x => x.Neighbourhood, nullable: true);
            Field(x => x.State, nullable: true);
        }
    }
}
