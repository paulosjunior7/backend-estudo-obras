using FluentValidation;
using Obras.Business.ConstructionAdvanceMoneyDomain.Request;
using Obras.Business.ConstructionDomain.Request;
namespace Obras.Api.Validators
{
    public class ConstructionAdvanceMoneyValidator : AbstractValidator<ConstructionAdvanceMoneyInput>
    {
        public ConstructionAdvanceMoneyValidator()
        {
            RuleFor(user => user.Date)
                .NotEmpty();

            RuleFor(user => user.Value)
                .NotEmpty();

            RuleFor(user => user.Active)
                .NotEmpty();
        }
    }
}

