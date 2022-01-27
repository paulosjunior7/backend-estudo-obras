using GraphQL;
using GraphQL.Types;
using Obras.Business.EmployeeDomain.Models;
using Obras.Business.EmployeeDomain.Services;
using Obras.Data;
using Obras.GraphQLModels.EmployeeDomain.InputTypes;
using Obras.GraphQLModels.EmployeeDomain.Types;

namespace Obras.GraphQLModels.EmployeeDomain.Mutations
{
    public class EmployeeMutation : ObjectGraphType
    {
        public EmployeeMutation(IEmployeeService employeeService, ObrasDBContext dBContext)
        {
            Name = nameof(EmployeeMutation);

            this.AuthorizeWith("LoggedIn");

            FieldAsync<EmployeeType>(
                name: "createEmployee",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<EmployeeInputType>> { Name = "employee" }),
                resolve: async context =>
                {
                    var employeeModel = context.GetArgument<EmployeeModel>("employee");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);

                    employeeModel.CompanyId = (int)(employeeModel.CompanyId == null ? user.CompanyId != null ? user.CompanyId : 0 : employeeModel.CompanyId);
                    employeeModel.ChangeUserId = userId;
                    employeeModel.RegistrationUserId = userId;

                    var expense = await employeeService.CreateAsync(employeeModel);
                    return expense;
                });

            FieldAsync<EmployeeType>(
                name: "updateEmployee",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<EmployeeInputType>> { Name = "employee" }),
                resolve: async context =>
                {
                    int employeeId = context.GetArgument<int>("id");
                    var employeeModel = context.GetArgument<EmployeeModel>("employee");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);

                    employeeModel.CompanyId = (int)(employeeModel.CompanyId == null ? user.CompanyId != null ? user.CompanyId : 0 : employeeModel.CompanyId);
                    employeeModel.ChangeUserId = userId;

                    return await employeeService.UpdateEmployeeAsync(employeeId, employeeModel);
                });
        }
    }
}
