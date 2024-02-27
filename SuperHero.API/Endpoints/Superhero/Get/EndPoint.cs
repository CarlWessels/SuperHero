using FastEndpoints;
using SuperHero.API.Data.Interfaces;

namespace SuperHero.API.Endpoints.Superhero.Get
{
    /// <summary>
    /// Endpoint for retrieving superhero details by ID.
    /// </summary>
    /// <remarks>
    /// This endpoint allows clients to retrieve details of a superhero by specifying its ID.
    /// </remarks>
    public class Endpoint(ISuperheroRepository repository, ILogger<Endpoint> logger) : Endpoint<Request, Response>
    {
        private readonly ISuperheroRepository _repository = repository;
        private readonly ILogger<Endpoint> _logger = logger;

        /// <inheritdoc/>
        public override void Configure()
        {
            _logger.LogDebug("Configuring endpoint: {Endpoint}", GetType().Name);
            Get("/api/superhero/get");
            AllowAnonymous();
            Summary(s => {
                s.Summary = "Get Superhero Details";
                s.Description = "This endpoint enables clients to retrieve details of a superhero by specifying its ID.";
            });
        }

        /// <inheritdoc/>
        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            _logger.LogDebug("Retrieving superhero details for ID: {SuperheroId}", req.SuperheroId);
            var superHero = await _repository.GetByIdAsync(req.SuperheroId);
            await SendAsync(new() { SuperHero = superHero }, cancellation: ct);
        }
    }
}
