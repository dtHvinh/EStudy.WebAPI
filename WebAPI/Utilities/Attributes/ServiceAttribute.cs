namespace WebAPI.Utilities.Attributes;

/// <summary>
/// Attribute to automatically register services with the dependency injection container
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class ServiceAttribute : Attribute
{
    /// <summary>
    /// The lifetime of the service registration
    /// </summary>
    public ServiceLifetime Lifetime { get; }

    /// <summary>
    /// The service type to register. If null, the implementation type is used to find interfaces.
    /// </summary>
    public Type? ServiceType { get; }

    /// <summary>
    /// Registers a service with the specified lifetime
    /// </summary>
    /// <param name="lifetime">The lifetime of the service</param>
    public ServiceAttribute(ServiceLifetime lifetime)
    {
        Lifetime = lifetime;
        ServiceType = null;
    }

    /// <summary>
    /// Registers a service with the specified lifetime and explicit service type
    /// </summary>
    /// <param name="lifetime">The lifetime of the service</param>
    /// <param name="serviceType">The service type to register</param>
    public ServiceAttribute(ServiceLifetime lifetime, Type serviceType)
    {
        Lifetime = lifetime;
        ServiceType = serviceType;
    }
}

