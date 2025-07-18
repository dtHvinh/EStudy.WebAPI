namespace WebAPI.Utilities.Converters.Contract;

public interface ICurrencyConverter<TSource, TResult>
{
    TResult ConvertTo(TSource source);
}
