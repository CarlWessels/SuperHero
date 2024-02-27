using FastEndpoints;
using SuperHero.API.Data.Interfaces;

namespace SuperHero.API.Endpoints.Superheroes.Search
{
    /// <summary>
    /// Endpoint for searching superheroes.
    /// </summary>
    /// <remarks>
    /// This endpoint allows clients to search for superheroes based on specific criteria.
    /// </remarks>
    public class Endpoint(ISuperheroRepository repository, ILogger<Endpoint> logger) : Endpoint<Request, Response>
    {
        private readonly ISuperheroRepository _repository = repository;
        private readonly ILogger<Endpoint> _logger = logger;

        /// <inheritdoc/>
        public override void Configure()
        {
            _logger.LogDebug("Configuring endpoint: {Endpoint}", GetType().Name);
            Get("/api/superheroes/search");
            AllowAnonymous();
            Summary(s => {
                s.Summary = "Search Superheroes";
                s.Description = "This endpoint enables clients to search for superheroes based on specific criteria.";
            });
        }

        /// <inheritdoc/>
        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            _logger.LogDebug("Searching for superheroes with criteria: {Search}", req.Search);
            var superheroes = await _repository.SearchSuperHeroAsync(req.Search);
            await SendAsync(new() { Superheroes = superheroes.ToList() }, cancellation: ct);
        }
    }
}
