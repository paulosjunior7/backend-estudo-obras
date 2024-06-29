using FluentValidation;
using Obras.Business.ConstructionInvestorDomain.Request;

namespace Obras.Api.Validators
{
    public class ConstructionInvestorValidator : AbstractValidator<ConstructionInvestorInput>
    {
        public ConstructionInvestorValidator()
        {

            RuleFor(user => user.PeopleId)
                .NotEmpty();

            RuleFor(user => user.Active)
                .NotEmpty();
        }
    }
}

