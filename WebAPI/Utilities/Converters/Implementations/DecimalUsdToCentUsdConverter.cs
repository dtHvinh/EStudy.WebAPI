using WebAPI.Utilities.Converters.Contract;

namespace WebAPI.Utilities.Converters.Implementations;

public class DecimalUsdToCentUsdConverter : ICurrencyConverter<decimal, long>
{
    public const int CentsPerDollar = 100;

    public long ConvertTo(decimal source)
    {
        return (long)Math.Round(source * CentsPerDollar);
    }
}
