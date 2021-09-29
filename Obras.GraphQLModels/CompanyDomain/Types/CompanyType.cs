namespace Obras.GraphQLModels.CompanyDomain.Types
{
    using GraphQL.Types;
    using Obras.Data.Entities;

    public class CompanyType : ObjectGraphType<Company>
    {
        public CompanyType()
        {
            Name = nameof(CompanyType);

            Field(x => x.Id);
            Field(x => x.Neighbourhood, nullable: true);
            Field(x => x.Number, nullable: true);
            Field(x => x.State, nullable: true);
            Field(x => x.Telephone, nullable: true);
            Field(x => x.ZipCode, nullable: true);
            Field(x => x.Active);
            Field(x => x.Address, nullable: true);
            Field(x => x.CellPhone, nullable: true);
            Field(x => x.ChangeDate, nullable: true);
            Field(x => x.City, nullable: true);
            Field(x => x.Cnpj, nullable: true);
            Field(x => x.Complement, nullable: true);
            Field(x => x.CorporateName, nullable: true);
            Field(x => x.CreationDate, nullable: true);
            Field(x => x.EMail, nullable: true);
            Field(x => x.FantasyName, nullable: true); 
        }
    }
}
