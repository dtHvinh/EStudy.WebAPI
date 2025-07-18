using WebAPI.Utilities.Attributes;

namespace WebAPI.Utilities.Currency;

// This class is responsible for providing application-specific currency properties, such as the application fee percentage.
// It can be extended to include more properties related to currency handling in the application.
// In the future, this should fetch these properties from a configuration file or a database to make them dynamic.
[Service(ServiceLifetime.Singleton)]
public class ApplicationCurrencyProperties
{
    public async Task<int> GetApplicationFeePercentage()
    {
        // This method should return the application fee percentage for the application.
        // For example, it could be a constant value or fetched from a configuration file or database.
        // Here, we return a hardcoded value of 10% as an example.
        return await Task.FromResult(10);
    }
}
