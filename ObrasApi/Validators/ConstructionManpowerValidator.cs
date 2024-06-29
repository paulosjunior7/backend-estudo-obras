using FluentValidation;
using Obras.Business.ConstructionAdvanceMoneyDomain.Request;
using Obras.Business.ConstructionBatchDomain.Request;
using Obras.Business.ConstructionDomain.Request;
using Obras.Business.ConstructionManpowerDomain.Request;

namespace Obras.Api.Validators
{
    public class ConstructionManpowerValidator : AbstractValidator<ConstructionManpowerInput>
    {
        public ConstructionManpowerValidator()
        {

            RuleFor(user => user.Value)
                .NotEmpty();

            RuleFor(user => user.Date)
                .NotEmpty();

            RuleFor(user => user.Active)
                .NotEmpty();
        }
    }
}

