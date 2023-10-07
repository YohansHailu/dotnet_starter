using System.Text;
using Newtonsoft.Json;

namespace Blog.Validation;
public class TisaneAbuseChecker : IAbuseChecker
{
    private readonly string _url;
    private readonly string _subscriptionKey;

    public TisaneAbuseChecker(string url, string subscriptionKey)
    {
        _url = url;
        _subscriptionKey = subscriptionKey;
    }

    public AbuseCheckResult CheckAbuse(string language, string content, object settings)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);

            dynamic data = new
            {
                language,
                content,
                settings
            };

            string jsonData = JsonConvert.SerializeObject(data);

            var response = client.PostAsync(_url, new StringContent(jsonData, Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

            if (!response.IsSuccessStatusCode)
            {
                return AbuseCheckResult.Failure("Something went wrong, please try again later");
            }

            string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (responseContent == null)
            {
                return AbuseCheckResult.Failure("Something went wrong, please try again later");
            }

            dynamic responseData = Newtonsoft.Json.JsonConvert.DeserializeObject(responseContent);

            if (responseData == null || responseData.abuse == null || responseData.abuse.Count <= 0)
            {
                return AbuseCheckResult.Success();
            }

            List<string> abusiveWords = new List<string>();
            foreach (dynamic row in responseData.abuse)
            {
                string type = row.type;
                abusiveWords.Add(type);
            }

            return AbuseCheckResult.Failure("Abusive words found: " + string.Join(",", abusiveWords));
        }
    }
}
