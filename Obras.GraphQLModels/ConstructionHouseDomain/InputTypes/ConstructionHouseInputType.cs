using GraphQL.Types;
using Obras.Business.ConstructionHouseDomain.Models;

namespace Obras.GraphQLModels.ConstructionHouseDomain.InputTypes
{
    public class ConstructionHouseInputType : InputObjectGraphType<ConstructionHouseModel>
    {
        public ConstructionHouseInputType()
        {
            Name = nameof(ConstructionHouseInputType);

            Field(x => x.PermeableArea, nullable: true);
            Field(x => x.ConstructionId, nullable: true);
            Field(x => x.FractionBatch, nullable: true);
            Field(x => x.BuildingArea, nullable: true);
            Field(x => x.Description);
            Field(x => x.EnergyConsumptionUnit, nullable: true);
            Field(x => x.SaleValue, nullable: true);
            Field(x => x.WaterConsumptionUnit, nullable: true);
            Field(x => x.Active);
        }
    }
}
