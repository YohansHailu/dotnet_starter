using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Blog.Validation;
public class AbusiveAttribute : ValidationAttribute
{
    private readonly string _url;
    private readonly string _language;
    private readonly string _subscriptionKey;

    public AbusiveAttribute(string url, string subscriptionKey, string language = "en")
    {
        _url = url;
        _subscriptionKey = subscriptionKey;
        _language = "en";
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        string content = value.ToString();
        var settings = JsonConvert.DeserializeObject("{}");

        IAbuseChecker abuseChecker = new TisaneAbuseChecker(_url, _subscriptionKey);
        var result = abuseChecker.CheckAbuse(_language, content, settings);

        if (result.IsValid)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(result.ErrorMessage);
    }
}
