namespace WebAPI.Utilities.ValueValidation;

public static class Guard
{
    public static T MustValue<T>(this T? value)
    {
        ArgumentNullException.ThrowIfNull(value, nameof(value));
        return value;
    }
}
