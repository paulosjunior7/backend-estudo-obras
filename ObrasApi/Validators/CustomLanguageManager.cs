using System;
namespace Obras.Api.Validators
{
    public class CustomLanguageManager : FluentValidation.Resources.LanguageManager
    {
        public CustomLanguageManager()
        {
            AddTranslation("en", "NotNullValidator", "'{PropertyName}' é obrigatório.");
            AddTranslation("en", "NotNullValidator", "'{PropertyName}' é obrigatório.");
        }
    }
}

