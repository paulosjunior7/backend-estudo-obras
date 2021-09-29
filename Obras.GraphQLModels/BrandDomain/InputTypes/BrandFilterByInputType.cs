using GraphQL.Types;
using Obras.Business.BrandDomain.Models;

namespace Obras.GraphQLModels.BrandDomainInputTypes
{
    public class BrandFilterByInputType : InputObjectGraphType<BrandFilter>
    {
        public BrandFilterByInputType()
        {
            Field(x => x.Id, nullable: true);
            Field(x => x.Description, nullable: true);
            Field(x => x.CompanyId, nullable: true);
            Field(x => x.Active, nullable: true);
        }
    }
}
