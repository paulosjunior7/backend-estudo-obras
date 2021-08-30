namespace Obras.GraphQLModels.Types
{
    using GraphQL.Types;
    using Obras.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CompanyType : ObjectGraphType<Company>
    {
        public CompanyType()
        {
            Name = nameof(CompanyType);

            Field(x => x.Id);
            Field(x => x.Neighbourhood);
            Field(x => x.Number);
            Field(x => x.State);
            Field(x => x.Telephone);
            Field(x => x.ZipCode);
            Field(x => x.Active);
            Field(x => x.Address);
            Field(x => x.CellPhone);
            Field(x => x.ChangeDate, nullable: true);
            Field(x => x.City);
            Field(x => x.Cnpj);
            Field(x => x.Complement);
            Field(x => x.CorporateName);
            Field(x => x.CreationDate, nullable: true);
            Field(x => x.EMail);
            Field(x => x.FantasyName); 
        }
    }
}
