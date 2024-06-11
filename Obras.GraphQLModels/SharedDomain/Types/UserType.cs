namespace Obras.GraphQLModels.SharedDomain.Types
{
    using GraphQL.Types;
    using Obras.Business.CompanyDomain.Services;
    using Obras.Data.Entities;

    public class UserType : ObjectGraphType<User>
    {
        public UserType(ICompanyService companyService)
        {
            Name = nameof(UserType);

            Field(x => x.Id);
            Field(x => x.UserName);
            Field(x => x.Email, nullable: true);
            Field(x => x.PhoneNumber, nullable: true);

        }
    }
}
