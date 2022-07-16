using GraphQL.Types;
using Obras.Business.OutsourcedDomain.Models;
using Obras.GraphQLModels.PeopleDomain.Enums;

namespace Obras.GraphQLModels.OutsourcedDomain.InputTypes
{
    public class OutsourcedInputType : InputObjectGraphType<OutsourcedModel>
    {
        public OutsourcedInputType()
        {
            Name = nameof(OutsourcedInputType);

            Field(x => x.Cnpj, nullable: true);
            Field(x => x.CorporateName);
            Field(x => x.FantasyName, nullable: true);
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
            Field(x => x.Cpf, nullable: true);
            Field<TypePeopleEnumType>("typePeople");
        }
    }
}
