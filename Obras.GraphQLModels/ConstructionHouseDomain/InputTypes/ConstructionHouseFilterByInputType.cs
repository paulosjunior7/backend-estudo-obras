using GraphQL.Types;
using Obras.Business.ConstructionHouseDomain.Models;

namespace Obras.GraphQLModels.ConstructionHouseDomain.InputTypes
{
    public class ConstructionHouseFilterByInputType : InputObjectGraphType<ConstructionHouseFilter>
    {
        public ConstructionHouseFilterByInputType()
        {
            Field(x => x.Id, nullable: true);
            Field(x => x.Description, nullable: true);
            Field(x => x.BuildingArea, nullable: true);
            Field(x => x.EnergyConsumptionUnit, nullable: true);
            Field(x => x.ConstructionId, nullable: true);
            Field(x => x.PermeableArea, nullable: true);
            Field(x => x.Registration, nullable: true);
            Field(x => x.SaleValue, nullable: true);
            Field(x => x.WaterConsumptionUnit, nullable: true);
        }
    }
}
