using FluentValidation;
using Obras.Business.ConstructionAdvanceMoneyDomain.Request;
using Obras.Business.ConstructionBatchDomain.Request;
using Obras.Business.ConstructionDomain.Request;
namespace Obras.Api.Validators
{
    public class ConstructionBatchValidator : AbstractValidator<ConstructionBatchInput>
    {
        public ConstructionBatchValidator()
        {

            RuleFor(user => user.Value)
                .NotEmpty();

            RuleFor(user => user.Active)
                .NotEmpty();
        }
    }
}

