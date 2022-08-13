using GraphQL.Types;
using Obras.Data;
using Obras.Data.Entities;
using Obras.GraphQLModels.ConstructionDomain.Types;
using Obras.GraphQLModels.SharedDomain.Types;

namespace Obras.GraphQLModels.ConstructionHouseDomain.Types
{
    public class ConstructionHouseType : ObjectGraphType<ConstructionHouse>
    {
        public ConstructionHouseType(ObrasDBContext dbContext)
        {
            Name = nameof(ConstructionHouseType);

            Field(x => x.Id);
            Field(x => x.Description);
            Field(x => x.ConstructionId, nullable: true);
            Field(x => x.PermeableArea, nullable: true);
            Field(x => x.FractionBatch, nullable: true);
            Field(x => x.BuildingArea, nullable: true);
            Field(x => x.EnergyConsumptionUnit, nullable: true);
            Field(x => x.SaleValue, nullable: true);
            Field(x => x.WaterConsumptionUnit, nullable: true);
            Field(x => x.Active);

            FieldAsync<UserType>(
                name: "changeUser",
                resolve: async context => await dbContext.User.FindAsync(context.Source.ChangeUserId));

            FieldAsync<UserType>(
                name: "registrationUser",
                resolve: async context => await dbContext.User.FindAsync(context.Source.RegistrationUserId));

            FieldAsync<ConstructionType>(
                name: "construction",
                resolve: async context => await dbContext.Constructions.FindAsync(context.Source.ConstructionId));
        }
    }
}
