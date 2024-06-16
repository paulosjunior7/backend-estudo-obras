using FluentValidation;
using Obras.Business.PeopleDomain.Request;
using Obras.Data.Enums;
using System;
namespace Obras.Api.Validators
{
    public class PeopleValidator : AbstractValidator<PeopleInput>
    {
        public PeopleValidator()
        {
            RuleFor(user => user.TypePeople)
                .NotEmpty().WithMessage("TypePeople é obrigatório.")
                .IsInEnum().WithMessage("Tipo de Pessoa invalida.");

            RuleFor(user => user.Constructor)
                .NotEmpty().WithMessage("Constructor é obrigatório.");

            RuleFor(user => user.Investor)
                .NotEmpty().WithMessage("Investor é obrigatório.");

            RuleFor(user => user.Client)
                .NotEmpty().WithMessage("Client é obrigatório.");

            RuleFor(user => user.Cpf)
                .MaximumLength(14).WithMessage("Tamanho máximo permitido para o CPF é de 14 caracteres.");

            RuleFor(user => user.Cnpj)
                .MaximumLength(18).WithMessage("Tamanho máximo permitido para o CNPJ é de 18 caracteres.");

            RuleFor(user => user.CorporateName)
                .NotEmpty().WithMessage("CorporateName é obrigatório.")
                .MaximumLength(100).WithMessage("Tamanho máximo permitido para o CorporateName é de 100 caracteres.");

            RuleFor(user => user.FantasyName)
                .MaximumLength(100).WithMessage("Tamanho máximo permitido para o FantasyName é de 100 caracteres.");

            RuleFor(user => user.ZipCode)
                .MaximumLength(10).WithMessage("Tamanho máximo permitido para o ZipCode é de 10 caracteres.");

            RuleFor(user => user.Address)
                .MaximumLength(100).WithMessage("Tamanho máximo permitido para o Address é de 100 caracteres.");

            RuleFor(user => user.Number)
                .MaximumLength(15).WithMessage("Tamanho máximo permitido para o Number é de 15 caracteres.");

            RuleFor(user => user.Neighbourhood)
                .MaximumLength(100).WithMessage("Tamanho máximo permitido para o Neighbourhood é de 100 caracteres.");

            RuleFor(user => user.City)
                .MaximumLength(50).WithMessage("Tamanho máximo permitido para o City é de 50 caracteres.");

            RuleFor(user => user.State)
                .MaximumLength(2).WithMessage("Tamanho máximo permitido para o State é de 2 caracteres.");

            RuleFor(user => user.Complement)
                .MaximumLength(100).WithMessage("Tamanho máximo permitido para o Complement é de 100 caracteres.");

            RuleFor(user => user.Telephone)
                .MaximumLength(18).WithMessage("Tamanho máximo permitido para o Telephone é de 18 caracteres.");

            RuleFor(user => user.CellPhone)
                .MaximumLength(18).WithMessage("Tamanho máximo permitido para o CellPhone é de 18 caracteres.");

            RuleFor(user => user.EMail)
                .MaximumLength(100).WithMessage("Tamanho máximo permitido para o EMail é de 100 caracteres.");

            RuleFor(user => user.Active)
                .NotEmpty().WithMessage("Active é obrigatório.");
        }
    }
}

