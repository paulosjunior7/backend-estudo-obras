using GraphQL.Types;
using Obras.Business.EmployeeDomain.Models;

namespace Obras.GraphQLModels.EmployeeDomain.InputTypes
{    
    public class  EmployeeFilterByInputType : InputObjectGraphType<EmployeeFilter>
    {
        public EmployeeFilterByInputType()
        {
            Field(x => x.Id, nullable: true);
            Field(x => x.Cpf, nullable: true);
            Field(x => x.Name, nullable: true);
            Field(x => x.Active, nullable: true);
            Field(x => x.City, nullable: true);
            Field(x => x.Neighbourhood, nullable: true);
            Field(x => x.State, nullable: true);
            Field(x => x.CompanyId, nullable: true);
        }
    }
}
