using FluentValidation;
using Obras.Business.ConstructionInvestorDomain.Request;
using Obras.Business.ConstructionMaterialDomain.Request;

namespace Obras.Api.Validators
{
    public class ConstructionMaterialValidator : AbstractValidator<ConstructionMaterialInput>
    {
        public ConstructionMaterialValidator()
        {

            RuleFor(user => user.PurchaseDate)
                .NotEmpty();

            RuleFor(user => user.Quantity)
                .NotEmpty();

            RuleFor(user => user.UnitPrice)
                .NotEmpty();

            RuleFor(user => user.ProductId)
                .NotEmpty();

            RuleFor(user => user.Active);
        }
    }
}

