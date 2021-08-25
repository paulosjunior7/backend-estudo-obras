using FluentValidation;
using ObrasApi.src.Company.BusinessRules.Requests;

namespace ObrasApi.src.Company.BusinessRules.Validators
{
    public interface ICompanyValidator : IValidator<UpsertCompanyRequest>
    {

    }
}