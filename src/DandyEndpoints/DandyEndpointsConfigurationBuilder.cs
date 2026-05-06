using System.Reflection;

namespace DandyEndpoints;

public sealed class DandyEndpointsConfigurationBuilder
{
    private readonly DandyEndpointsConfiguration _configuration = new();

    internal DandyEndpointsConfigurationBuilder()
    {
    }

    public DandyEndpointsConfigurationBuilder ScanInAssemblies(params IEnumerable<Assembly> assemblies)
    {
        _configuration.Assemblies = assemblies.ToList();
        return this;
    }

    internal DandyEndpointsConfiguration Build() => _configuration;
}
