namespace Obras.GraphQLModels.Types
{
    using GraphQL.Types;
    using Obras.Business.Services;
    using Obras.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UserType : ObjectGraphType<User>
    {
        public UserType(ICompanyService companyService)
        {
            Name = nameof(UserType);

            Field(x => x.Id);
            Field(x => x.UserName);
            Field(x => x.Email, nullable: true);
            Field(x => x.PhoneNumber, nullable: true);

            Field<CompanyType>(
                name: "company",
                resolve: context =>
                {
                    int valor = (int)(context.Source.CompanyId != null ? context.Source.CompanyId : 0);
                    return companyService.GetCompanyId(valor);
                });
        }
    }
}
