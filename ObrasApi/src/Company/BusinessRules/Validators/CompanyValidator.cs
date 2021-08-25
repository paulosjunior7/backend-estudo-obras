using FluentValidation;
using ObrasApi.src.Company.BusinessRules.Requests;

namespace ObrasApi.src.Company.BusinessRules.Validators
{
    public class CompanyValidator : AbstractValidator<UpsertCompanyRequest>, ICompanyValidator
    {
        public CompanyValidator()
        {
            RuleFor(t => t.Cnpj)
                .NotEmpty()
                .NotNull()
                .MinimumLength(14)
                .MaximumLength(18)
                .WithName("CNPJ");

            RuleFor(t => t.CorporateName)
                .NotEmpty()
                .NotNull()
                .MinimumLength(5)
                .MaximumLength(150)
                .WithName("Razão Social");

            RuleFor(t => t.EMail)
                .NotEmpty()
                .NotEmpty()
                .EmailAddress()
                .WithName("E-mail");
        }
    }
}