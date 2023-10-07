using System.ComponentModel.DataAnnotations;

namespace Blog.Validation;
public class MinWordsAttribute : ValidationAttribute
{
    private readonly int _minWords;

    public MinWordsAttribute(int minWords)
    {
        _minWords = minWords;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null)
        {
            var content = value.ToString();
            var wordCount = content.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;

            if (wordCount < _minWords)
            {
                return new ValidationResult($"Content should have at least {_minWords} words.");
            }
        }

        return ValidationResult.Success;
    }
}
