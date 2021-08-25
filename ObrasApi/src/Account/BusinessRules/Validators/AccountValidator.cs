using FluentValidation;
using ObrasApi.src.Account.BusinessRules.Requests;

namespace ObrasApi.src.Account.BusinessRules.Validators
{
    public class AccountValidator : AbstractValidator<UpsertAccountRequest>, IAccountValidator
    {
        public AccountValidator()
        {
            RuleFor(t => t.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(100)
                .WithName("Nome");

            RuleFor(t => t.EMail)
                .NotEmpty()
                .NotNull()
                .EmailAddress()
                .WithName("E-Mail");

            RuleFor(t => t.Password)
                .NotEmpty()
                .NotNull()
                .MinimumLength(6)
                .MaximumLength(50)
                .WithName("Senha");
        }
    }
}