# Whats Andy?
A tiny framework for encapsulating ASP.NET Core endpoints into separate classes and registering it automatically, to allow a more seamless implementation of vertical slicing.

# Why does Andy exist?
I like to encapsulate endpoints and their related components in one static class to implement vertical slicing. In 'vanilla' ASP.NET Core you could (A) implement a controller as a subclass of that static class and add an endpoint. Alternatively you could (B) write a static extension to the *IEndpointRouteBuilder* to map your endpoint as a minimal API endpoint. Variant (A) would allow automatic controller registration if configured correctly. Variant (B) doesnt allow automatic registrations via scanning at all and requires developers to manually call methods. Both approaches are fine and free a project of third-party dependencies.

I however quite like the pattern of creating a dedicated class for every endpoint and adding it as a private subclass to my static 'use case class' like shown below and since I want to use that pattern in more than just one project I want to create a small package that I am maintaining primarily for myself. If your like it, cool! If you dont, dont worry thats fine as well.

# What pattern does Andy aim for?
The example below features my own version of the classic [MediatR](https://github.com/LuckyPennySoftware/MediatR) package, [OpenMediator](https://github.com/Nico1395/open-messenger) which also only really exists because I want to use it across a few of my projects.

```cs
internal static class GetTeamById
{
    private sealed class Endpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/teams/{teamId}", async ([FromServices] IMediator mediator, Guid teamId, CancellationToken cancellationToken) =>
            {
                var response = await mediator.SendAsync<Query, Team>(new Query(teamId), cancellationToken);
                return response.ToResult();
            });
        }
    }

    private sealed record Query(Guid TeamId) : IQuery<Team>;

    private sealed class QueryHandler : IQueryHandler<Query, Team>
    {
        public Task<IQueryResponse<Team>> HandleAsync(Query request, CancellationToken cancellationToken)
        {
            // Implementation of the use case
        }
    }

    // More private classes if need be
}
```

# How is Andy being set up?
Exactly like you would expect from any .NET Core framework. Have a quick look:

```cs

```
