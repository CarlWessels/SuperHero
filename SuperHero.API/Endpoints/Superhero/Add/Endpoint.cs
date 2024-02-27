using FastEndpoints;
using SuperHero.API.Data.Interfaces;

namespace SuperHero.API.Endpoints.Superhero.Add
{
    /// <summary>
    /// Endpoint for adding a superhero to the system.
    /// </summary>
    /// <remarks>
    /// This endpoint allows clients to add a new superhero to the system.
    /// </remarks>
    public class Endpoint(ISuperheroRepository repository, ILogger<Endpoint> logger) : Endpoint<Request, Response>
    {
        private readonly ISuperheroRepository _repository = repository;
        private readonly ILogger<Endpoint> _logger = logger;

        /// <inheritdoc/>
        public override void Configure()
        {
            _logger.LogDebug("Configuring endpoint: {Endpoint}", GetType().Name);
            Post("/api/superhero/add");
            AllowAnonymous();
            Summary(s => {
                s.Summary = "Add a Superhero";
                s.Description = "This endpoint enables clients to add a new superhero to the system.";
            });
        }

        /// <inheritdoc/>
        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            _logger.LogDebug("Adding superhero: {Superhero}", req.Superhero);
            var inserted = await _repository.InsertAsync(req.Superhero);
            await SendAsync(new() { SuperHero = inserted }, cancellation: ct);
        }
    }
}
