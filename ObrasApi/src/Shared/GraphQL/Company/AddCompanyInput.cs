using System;

namespace ObrasApi.src.Shared.GraphQL.Company
{
    public record AddCompanyInput(
        Guid? id,
        string cnpj,
        string corporateName,
        string fantasyName,
        string zipCode,
        string address,
        string number,
        string neighbourhood,
        string city,
        string state,
        string complement,
        string telephone,
        string cellPhone,
        string eMail,
        bool active,
        DateTime? changeDate,
        DateTime? creationDate
    );
}