using Microsoft.AspNetCore.Routing;

namespace DandyEndpoints;

public interface IEndpoint
{
    void Map(IEndpointRouteBuilder app);
}
