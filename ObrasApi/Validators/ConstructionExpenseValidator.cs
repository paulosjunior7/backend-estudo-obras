using FluentValidation;
using Obras.Business.ConstructionDocumentationDomain.Request;
using Obras.Business.ConstructionExpenseDomain.Request;
using System;
namespace Obras.Api.Validators
{
    public class ConstructionExpenseValidator : AbstractValidator<ConstructionExpenseInput>
    {
        public ConstructionExpenseValidator()
        {
            RuleFor(user => user.Date)
                .NotEmpty();

            RuleFor(user => user.Value)
                .NotEmpty();

            RuleFor(user => user.ExpenseId)
                .NotEmpty();

            RuleFor(user => user.Active)
                .NotEmpty();
        }
    }
}

