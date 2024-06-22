using FluentValidation;
using Obras.Business.ConstructionDocumentationDomain.Request;
namespace Obras.Api.Validators
{
    public class ConstructionDocumentationValidator : AbstractValidator<ConstructionDocumentationInput>
    {
        public ConstructionDocumentationValidator()
        {
            RuleFor(user => user.Date)
                .NotEmpty();

            RuleFor(user => user.Value)
                .NotEmpty();

            RuleFor(user => user.DocumentationId)
                .NotEmpty();

            RuleFor(user => user.Active)
                .NotEmpty();
        }
    }
}

