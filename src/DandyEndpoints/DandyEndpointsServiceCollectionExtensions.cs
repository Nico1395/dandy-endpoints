using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DandyEndpoints;

public static class DandyEndpointsServiceCollectionExtensions
{
    public static IServiceCollection AddDandyEndpoints(this IServiceCollection services, Action<DandyEndpointsConfigurationBuilder>? configuration = null)
    {
        var builder = new DandyEndpointsConfigurationBuilder();
        configuration?.Invoke(builder);
        var cfg = builder.Build();

        ScanForEndpoints(services, cfg);

        return services;
    }

    private static void ScanForEndpoints(IServiceCollection services, DandyEndpointsConfiguration cfg)
    {
        var endpointServiceDescriptors = cfg.Assemblies
            .SelectMany(a => a.DefinedTypes)
            .Where(type =>
                type is { IsAbstract: false, IsInterface: false } &&
                type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type));
        services.TryAddEnumerable(endpointServiceDescriptors);
    }
}
