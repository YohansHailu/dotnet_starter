
namespace Blog.Validation;
public interface IAbuseChecker
{
    AbuseCheckResult CheckAbuse(string language, string content, object settings);
}

public class AbuseCheckResult
{
    public bool IsValid { get; }
    public string ErrorMessage { get; }

    private AbuseCheckResult(bool isValid, string errorMessage)
    {
        IsValid = isValid;
        ErrorMessage = errorMessage;
    }

    public static AbuseCheckResult Success()
    {
        return new AbuseCheckResult(true, null);
    }

    public static AbuseCheckResult Failure(string errorMessage)
    {
        return new AbuseCheckResult(false, errorMessage);
    }
}
