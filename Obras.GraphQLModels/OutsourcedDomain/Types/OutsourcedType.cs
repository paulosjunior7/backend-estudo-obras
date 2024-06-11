﻿using GraphQL.Types;
using Obras.Business.PeopleDomain.Services;
using Obras.Data;
using Obras.Data.Entities;
using Obras.GraphQLModels.SharedDomain.Types;

namespace Obras.GraphQLModels.OutsourcedDomain.Types
{
    public class OutsourcedType : ObjectGraphType<Outsourced>
    {
        public OutsourcedType(ObrasDBContext dbContext)
        {
            Name = nameof(OutsourcedType);

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
            Field(x => x.Cpf, nullable: true);
            Field(x => x.Complement, nullable: true);
            Field(x => x.CorporateName, nullable: true);
            Field(x => x.CreationDate, nullable: true);
            Field(x => x.EMail, nullable: true);
            Field(x => x.FantasyName, nullable: true);

            Field<StringGraphType>(
                name: "typePeople",
                resolve: context => context.Source.TypePeople.ToString());

            FieldAsync<UserType>(
                name: "changeUser",
                resolve: async context => await dbContext.User.FindAsync(context.Source.ChangeUserId));

            FieldAsync<UserType>(
                name: "registrationUser",
                resolve: async context => await dbContext.User.FindAsync(context.Source.RegistrationUserId));
        }
    }
}
