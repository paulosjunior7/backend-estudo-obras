using GraphQL.Types;
using Obras.Business.EmployeeDomain.Enums;

namespace Obras.GraphQLModels.EmployeeDomain.Enums
{
    public class EmployeeSortingFieldsEnumType : EnumerationGraphType<EmployeeSortingFields>
    {
        public EmployeeSortingFieldsEnumType()
        {
            Name = nameof(EmployeeSortingFieldsEnumType);
        }
    }
}
