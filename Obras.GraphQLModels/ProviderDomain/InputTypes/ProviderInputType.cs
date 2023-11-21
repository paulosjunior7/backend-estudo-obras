namespace Obras.GraphQLModels.ProviderDomain.InputTypes
{
    using GraphQL.Types;
    using Obras.Business.ProviderDomain.Models;
    using Obras.GraphQLModels.PeopleDomain.Enums;


    public class ProviderInputType : InputObjectGraphType<ProviderModel>
    {
        public ProviderInputType()
        {
            Name = nameof(ProviderInputType);

            Field(x => x.Cnpj);
            Field(x => x.Cpf, nullable: true);
            Field<TypePeopleEnumType>("typePeople");
            Field(x => x.Name);
            Field(x => x.CompanyId, nullable: true);
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
        }
    }
}
