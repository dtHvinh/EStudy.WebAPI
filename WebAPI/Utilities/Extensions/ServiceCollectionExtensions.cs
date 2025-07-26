using System.Collections.Immutable;
using System.Reflection;
using WebAPI.Utilities.Attributes;

namespace WebAPI.Utilities.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Scans the specified assembly for types marked with ServiceRegistrationAttribute
    /// and registers them with the DI container
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="assembly">The assembly to scan (defaults to calling assembly)</param>
    /// <returns>The service collection for chaining</returns>
    public static IServiceCollection AddServicesFromAssembly(this IServiceCollection services, Assembly? assembly = null)
    {
        assembly ??= Assembly.GetCallingAssembly();

        var typesWithAttribute = assembly.GetTypes()
            .Where(type => type.IsClass && !type.IsAbstract && type.GetCustomAttribute<ServiceAttribute>() != null).ToImmutableArray();

        foreach (var implementationType in typesWithAttribute)
        {
            var attribute = implementationType.GetCustomAttribute<ServiceAttribute>()!;

            // If a specific service type is provided, register that
            if (attribute.ServiceType != null)
            {
                services.Add(new ServiceDescriptor(
                    attribute.ServiceType,
                    implementationType,
                    attribute.Lifetime));

                continue;
            }

            // Otherwise, find interfaces that match naming convention (IService -> Service)
            var interfaces = implementationType.GetInterfaces();
            var matchingInterface = interfaces.FirstOrDefault(i =>
                i.Name == "I" + implementationType.Name);

            if (matchingInterface != null)
            {
                // Register with the matching interface
                services.Add(new ServiceDescriptor(
                    matchingInterface,
                    implementationType,
                    attribute.Lifetime));
            }
            else
            {
                // No matching interface found, register as self
                services.Add(new ServiceDescriptor(
                    implementationType,
                    implementationType,
                    attribute.Lifetime));
            }
        }

        return services;
    }
}