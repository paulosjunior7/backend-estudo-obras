using FluentValidation;
using Obras.Business.ConstructionDomain.Request;
using Obras.Business.EmployeeDomain.Request;
using Obras.Business.PeopleDomain.Request;
using Obras.Data.Enums;
using System;
namespace Obras.Api.Validators
{
    public class ConstructionValidator : AbstractValidator<ConstructionInput>
    {
        public ConstructionValidator()
        {
            RuleFor(user => user.Identifier)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(user => user.ZipCode)
                .MaximumLength(10);

            RuleFor(user => user.Address)
                .MaximumLength(100);

            RuleFor(user => user.Number)
                .MaximumLength(15);

            RuleFor(user => user.Neighbourhood)
                .MaximumLength(100);

            RuleFor(user => user.City)
                .MaximumLength(50);

            RuleFor(user => user.State)
                .MaximumLength(2);

            RuleFor(user => user.Complement)
                .MaximumLength(100);

            RuleFor(user => user.Active)
                .NotEmpty();
        }
    }
}

