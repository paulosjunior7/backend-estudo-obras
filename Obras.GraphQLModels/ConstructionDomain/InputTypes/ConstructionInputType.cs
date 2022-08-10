using GraphQL.Types;
using Obras.Business.ConstructionDomain.Models;
using Obras.GraphQLModels.ConstructionDomain.Enums;

namespace Obras.GraphQLModels.ConstructionDomain.InputTypes
{
    public class ConstructionInputType : InputObjectGraphType<ConstructionModel>
    {
        public ConstructionInputType()
        {
            Name = nameof(ConstructionInputType);

            Field(x => x.Address);
            Field(x => x.Art);
            Field(x => x.BatchArea);
            Field(x => x.BuildingArea);
            Field(x => x.City);
            Field(x => x.Cno);
            Field(x => x.Complement);
            Field(x => x.DateBegin);
            Field(x => x.DateEnd, nullable: true);
            Field(x => x.Identifier);
            Field(x => x.Latitude);
            Field(x => x.License);
            Field(x => x.Longitude);
            Field(x => x.MotherEnrollment, nullable: true);
            Field(x => x.MunicipalRegistration, nullable: true);
            Field(x => x.Neighbourhood, nullable: true);
            Field(x => x.Number, nullable: true);
            Field(x => x.SaleValue, nullable: true);
            Field(x => x.State);
            Field<StatusConstructionEnumType>("statusConstruction");
            Field(x => x.UndergroundUse, nullable: true);
            Field(x => x.ZipCode, nullable: true);
            Field(x => x.Active);
        }
    }
}
