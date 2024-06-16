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
                .NotEmpty().WithMessage("Identifier é obrigatório.")
                .MaximumLength(50).WithMessage("Tamanho máximo permitido é de 50 caracteres.");

            RuleFor(user => user.ZipCode)
                .MaximumLength(10).WithMessage("Tamanho máximo permitido é de 10 caracteres.");

            RuleFor(user => user.Address)
                .MaximumLength(100).WithMessage("Tamanho máximo permitido é de 100 caracteres.");

            RuleFor(user => user.Number)
                .MaximumLength(15).WithMessage("Tamanho máximo permitido é de 15 caracteres.");

            RuleFor(user => user.Neighbourhood)
                .MaximumLength(100).WithMessage("Tamanho máximo permitido é de 100 caracteres.");

            RuleFor(user => user.City)
                .MaximumLength(50).WithMessage("Tamanho máximo permitido é de 50 caracteres.");

            RuleFor(user => user.State)
                .MaximumLength(2).WithMessage("Tamanho máximo permitido é de 2 caracteres.");

            RuleFor(user => user.Complement)
                .MaximumLength(100).WithMessage("Tamanho máximo permitido é de 100 caracteres.");

            RuleFor(user => user.Active)
                .NotEmpty().WithMessage("Active é obrigatório.");
        }
    }
}

