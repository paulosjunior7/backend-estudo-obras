using FluentValidation;
using ObrasApi.src.Account.BusinessRules.Requests;

namespace ObrasApi.src.Account.BusinessRules.Validators
{
    public interface IAccountValidator : IValidator<UpsertAccountRequest>
    {

    }
}