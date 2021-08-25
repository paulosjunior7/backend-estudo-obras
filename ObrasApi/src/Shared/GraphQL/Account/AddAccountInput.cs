using System;

namespace ObrasApi.src.Shared.GraphQL.Account
{
    public record AddAccountInput(
        Guid? id,
        string name,
        string eMail,
        string password,
        Guid idCompany,
        bool active,
        DateTime? changeDate,
        DateTime? creationDate
    );
}