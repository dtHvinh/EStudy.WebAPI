using System.Collections.Specialized;
using System.Web;
using WebAPI.Models.Base;
using WebAPI.Models.Contract;

namespace WebAPI.Utilities.Extensions;

public static class ObjectExtensions
{
    /// <summary>
    /// Ensures return the value larger or equal to the target
    /// </summary>
    public static int EnsureLargerOrEqual(this int current, int target) => Math.Max(current, target);

    /// <summary>
    /// Ensures return the value will be in the given range
    /// </summary>
    public static int EnsureInRange(this int current, int min, int max) => Math.Clamp(current, min, max);

    public static bool IsBelongTo(this IBelongToUser<int> entity, string userId) => entity.AuthorId == int.Parse(userId);

    /// <summary>
    /// Either nothing or short than given length
    /// </summary>
    public static bool NothingOrShortThan(this string value, int max) => string.IsNullOrEmpty(value) || value.Length <= max;

    public static string ToQueryString(this NameValueCollection nvc)
    {
        var array = (
            from key in nvc.AllKeys
            from value in nvc.GetValues(key) ?? []
            select string.Format(
            "{0}={1}",
            HttpUtility.UrlEncode(key),
            HttpUtility.UrlEncode(value))
            ).ToArray();
        return "?" + string.Join("&", array);
    }

    public static IReadonlySupportResponse SetReadonly(this IReadonlySupportResponse response, bool isReadonly)
    {
        response.IsReadonly = isReadonly;
        return response;
    }
}
