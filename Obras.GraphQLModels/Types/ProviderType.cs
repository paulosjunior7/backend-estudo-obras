﻿namespace Obras.GraphQLModels.Types
{
    using GraphQL.Types;
    using Obras.Data;
    using Obras.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ProviderType : ObjectGraphType<Provider>
    {
        public ProviderType(ObrasDBContext dbContext)
        {
            Name = nameof(ProviderType);

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
            Field(x => x.Name, nullable: true);
            Field(x => x.CreationDate, nullable: true);
            Field(x => x.EMail, nullable: true);

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
