using GraphQL.Types;
using Obras.Business.EmployeeDomain.Models;

namespace Obras.GraphQLModels.EmployeeDomain.InputTypes
{
    public class EmployeeInputType : InputObjectGraphType<EmployeeModel>
    {
        public EmployeeInputType()
        {
            Name = nameof(EmployeeInputType);

            Field(x => x.Cpf, nullable: true);
            Field(x => x.Name);
            Field(x => x.Active);
            Field(x => x.Address, nullable: true);
            Field(x => x.CellPhone, nullable: true);
            Field(x => x.City, nullable: true);
            Field(x => x.Complement, nullable: true);
            Field(x => x.EMail, nullable: true);
            Field(x => x.Neighbourhood, nullable: true);
            Field(x => x.Number, nullable: true);
            Field(x => x.State, nullable: true);
            Field(x => x.Telephone, nullable: true);
            Field(x => x.ZipCode, nullable: true);
            Field(x => x.ResponsibilityId, nullable: false);
        }
    }
}
