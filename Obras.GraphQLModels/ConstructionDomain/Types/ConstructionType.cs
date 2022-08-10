using GraphQL.Types;
using Obras.Data;
using Obras.Data.Entities;
using Obras.GraphQLModels.CompanyDomain.Types;
using Obras.GraphQLModels.SharedDomain.Types;

namespace Obras.GraphQLModels.ConstructionDomain.Types
{
    public class ConstructionType : ObjectGraphType<Construction>
    {
        public ConstructionType(ObrasDBContext dbContext)
        {
            Name = nameof(ConstructionType);

            Field(x => x.Address);
            Field(x => x.Art, nullable: true);
            Field(x => x.BatchArea, nullable: true);
            Field(x => x.BuildingArea, nullable: true);
            Field(x => x.City);
            Field(x => x.Cno, nullable: true);
            Field(x => x.Complement);
            Field(x => x.DateBegin);
            Field(x => x.DateEnd, nullable: true);
            Field(x => x.Identifier);
            Field(x => x.Latitude, nullable: true);
            Field(x => x.License, nullable: true);
            Field(x => x.Longitude, nullable: true);
            Field(x => x.MotherEnrollment, nullable: true);
            Field(x => x.MunicipalRegistration, nullable: true);
            Field(x => x.Neighbourhood, nullable: true);
            Field(x => x.Number, nullable: true);
            Field(x => x.SaleValue, nullable: true);
            Field(x => x.State);
            Field(x => x.UndergroundUse, nullable: true);
            Field(x => x.ZipCode, nullable: true);
            Field(x => x.Active);

            Field<StringGraphType>(
                name: "statusConstruction",
                resolve: context => context.Source.StatusConstruction.ToString());

            FieldAsync<UserType>(
                name: "changeUser",
                resolve: async context => await dbContext.User.FindAsync(context.Source.ChangeUserId));

            FieldAsync<UserType>(
                name: "registrationUser",
                resolve: async context => await dbContext.User.FindAsync(context.Source.RegistrationUserId));

            FieldAsync<CompanyType>(
                name: "company",
                resolve: async context => await dbContext.Companies.FindAsync(context.Source.CompanyId));
        }
    }
}
