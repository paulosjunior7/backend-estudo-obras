using GraphQL.Types;
using Obras.Business.EmployeeDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.GraphQLModels.EmployeeDomain.Enums;
using Obras.GraphQLModels.SharedDomain.Enums;

namespace Obras.GraphQLModels.EmployeeDomain.InputTypes
{
    public class EmployeeByInputType : InputObjectGraphType<SortingDetails<EmployeeSortingFields>>
    {
        public EmployeeByInputType()
        {
            Field<EmployeeSortingFieldsEnumType>("field", resolve: context => context.Source.Field);
            Field<SortingDirectionEnumType>("direction", resolve: context => context.Source.Direction);
        }
    }
}
