using System.Reflection;

namespace DandyEndpoints;

internal sealed class DandyEndpointsConfiguration
{
    public IReadOnlyList<Assembly> Assemblies { get; internal set; } = [];
}
