using FluentValidation;
using Obras.Business.PeopleDomain.Request;
using Obras.Data.Entities;
using Obras.Data.Enums;
using System;
namespace Obras.Api.Validators
{
    public class PeopleValidator : AbstractValidator<PeopleInput>
    {
        public PeopleValidator()
        {
            RuleFor(user => user.TypePeople)
                .NotEmpty()
                .IsInEnum();

            RuleFor(user => user)
                .Must(this.HaveAtLeastOneTrueField)
                .WithMessage("Pelo menos um dos campos (Construtor, Investidor, Cliente) deve ser verdadeiro.");

            RuleFor(user => user.Cpf)
                .MaximumLength(14);

            RuleFor(user => user.Cnpj)
                .MaximumLength(18);

            RuleFor(user => user.CorporateName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(user => user.FantasyName)
                .MaximumLength(100);

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

            RuleFor(user => user.Telephone)
                .MaximumLength(18);

            RuleFor(user => user.CellPhone)
                .MaximumLength(18);

            RuleFor(user => user.EMail)
                .MaximumLength(100);

            RuleFor(user => user.Active)
                .NotEmpty();
        }

        private bool HaveAtLeastOneTrueField(PeopleInput user)
        {
            return user.Constructor || user.Investor || user.Client;
        }
    }
}

