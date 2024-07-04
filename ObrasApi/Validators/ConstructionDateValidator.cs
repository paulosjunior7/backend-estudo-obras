using FluentValidation;
using Obras.Business.ConstructionDomain.Request;
using Obras.Business.EmployeeDomain.Request;
using Obras.Business.PeopleDomain.Request;
using Obras.Data.Enums;
using System;
namespace Obras.Api.Validators
{
    public class ConstructionDateValidator : AbstractValidator<ConstructionDateInput>
    {
        public ConstructionDateValidator()
        {
            RuleFor(user => user.Identifier)
                .MaximumLength(50);
        }
    }
}

