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
                .NotEmpty()
                .IsInEnum();

            RuleFor(user => user.Constructor)
                .NotEmpty();

            RuleFor(user => user.Investor)
                .NotEmpty();

            RuleFor(user => user.Client)
                .NotEmpty();

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
    }
}

