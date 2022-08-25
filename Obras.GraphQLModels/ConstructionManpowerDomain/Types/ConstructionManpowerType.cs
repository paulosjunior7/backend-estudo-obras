using GraphQL.Types;
using Obras.Data;
using Obras.Data.Entities;
using Obras.GraphQLModels.ConstructionDomain.Types;
using Obras.GraphQLModels.ConstructionInvestorDomain.Types;
using Obras.GraphQLModels.EmployeeDomain.Types;
using Obras.GraphQLModels.OutsourcedDomain.Types;
using Obras.GraphQLModels.SharedDomain.Types;

namespace Obras.GraphQLModels.ConstructionManpowerDomain.Types
{
    public class ConstructionManpowerType: ObjectGraphType<ConstructionManpower>
    {
        public ConstructionManpowerType(ObrasDBContext dbContext)
        {
            Name = nameof(ConstructionManpowerType);

            Field(x => x.Id);
            Field(x => x.OutsourcedId);
            Field(x => x.Value);
            Field(x => x.Date);
            Field(x => x.ConstructionInvestorId);
            Field(x => x.EmployeeId);
            Field(x => x.Active);

            FieldAsync<UserType>(
                name: "changeUser",
                resolve: async context => await dbContext.User.FindAsync(context.Source.ChangeUserId));

            FieldAsync<UserType>(
                name: "registrationUser",
                resolve: async context => await dbContext.User.FindAsync(context.Source.RegistrationUserId));

            FieldAsync<ConstructionInvestorType>(
                name: "constructionInvestor",
                resolve: async context => await dbContext.ConstructionInvestors.FindAsync(context.Source.ConstructionInvestorId));

            FieldAsync<EmployeeType>(
                name: "employee",
                resolve: async context => await dbContext.Employees.FindAsync(context.Source.EmployeeId));

            FieldAsync<OutsourcedType>(
                name: "outsourced",
                resolve: async context => await dbContext.Outsourseds.FindAsync(context.Source.OutsourcedId));

            FieldAsync<ConstructionType>(
                name: "construction",
                resolve: async context => await dbContext.Constructions.FindAsync(context.Source.ConstructionId));
        }
    }
}
