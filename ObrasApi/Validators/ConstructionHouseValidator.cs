using FluentValidation;
using Obras.Business.ConstructionHouseDomain.Request;
using System;
namespace Obras.Api.Validators
{
    public class ConstructionHouseValidator : AbstractValidator<ConstructionHouseInput>
    {
        public ConstructionHouseValidator()
        {
            RuleFor(user => user.Description)
                .MaximumLength(500)
                .NotEmpty();

            RuleFor(user => user.Registration)
                .MaximumLength(20);

            RuleFor(user => user.EnergyConsumptionUnit)
                .MaximumLength(20);

            RuleFor(user => user.WaterConsumptionUnit)
                .MaximumLength(20);

            RuleFor(user => user.Active)
                .NotEmpty();
        }
    }
}

