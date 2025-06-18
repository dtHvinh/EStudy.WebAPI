namespace WebAPI.Utilities.Extensions;

public static class Fn
{
    private static readonly Random _random = new();

    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string([.. Enumerable.Repeat(chars, length).Select(s => s[_random.Next(s.Length)])]);
    }
}
