namespace Core.utils;
public class Utils
{
    public static string RandomString(int length)
    {
        var randomString = new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", length)
            .Select(s => s[new Random().Next(s.Length)]).ToArray());

        return randomString;
    }

}
