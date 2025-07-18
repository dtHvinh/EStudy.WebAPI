using System.Numerics;
using WebAPI.Utilities.Converters.Contract;

namespace WebAPI.Utilities.Extensions;

public static class CurrencyExtensions
{
    public static TResult CurrencyConvert<TSource, TResult>(this TSource source, ICurrencyConverter<TSource, TResult> converter)
        where TSource : INumber<TSource>
        where TResult : INumber<TResult>
    {
        return converter.ConvertTo(source);
    }

    public static TValue GetPercentage<TValue>(this TValue amount, int percentage)
        where TValue : INumber<TValue>
    {
        var percentValue = TValue.CreateChecked(percentage);
        var hundred = TValue.CreateChecked(100);

        return (amount * percentValue) / hundred;
    }
}
