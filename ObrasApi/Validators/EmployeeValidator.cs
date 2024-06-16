using FluentValidation;
using Obras.Business.EmployeeDomain.Request;
using Obras.Business.PeopleDomain.Request;
using Obras.Data.Enums;
using System;
namespace Obras.Api.Validators
{
    public class EmployeeValidator : AbstractValidator<EmployeeInput>
    {
        public EmployeeValidator()
        {
            RuleFor(user => user.TypePeople)
                .NotEmpty().WithMessage("TypePeople é obrigatório.")
                .IsInEnum().WithMessage("Tipo de Pessoa invalida.");

            RuleFor(user => user.Cpf)
                .MaximumLength(14).WithMessage("Tamanho máximo permitido é de 14 caracteres.");

            RuleFor(user => user.Cnpj)
                .MaximumLength(18).WithMessage("Tamanho máximo permitido é de 18 caracteres.");

            RuleFor(user => user.Name)
                .NotEmpty().WithMessage("Name é obrigatório.")
                .MaximumLength(100).WithMessage("Tamanho máximo permitido é de 100 caracteres.");

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

            RuleFor(user => user.Telephone)
                .MaximumLength(18).WithMessage("Tamanho máximo permitido é de 18 caracteres.");

            RuleFor(user => user.CellPhone)
                .MaximumLength(18).WithMessage("Tamanho máximo permitido é de 18 caracteres.");

            RuleFor(user => user.EMail)
                .MaximumLength(100).WithMessage("Tamanho máximo permitido é de 100 caracteres.");

            RuleFor(user => user.Active)
                .NotEmpty().WithMessage("Active é obrigatório.");
        }
    }
}

