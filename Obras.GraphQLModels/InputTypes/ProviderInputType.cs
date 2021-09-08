namespace Obras.GraphQLModels.InputTypes
{
    using GraphQL.Types;
    using Obras.Business.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ProviderInputType : InputObjectGraphType<ProviderModel>
    {
        public ProviderInputType()
        {
            Name = nameof(ProviderInputType);

            Field(x => x.Cnpj);
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
