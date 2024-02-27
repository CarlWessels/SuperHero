using FastEndpoints;
using SuperHero.API.Data.Interfaces;

namespace SuperHero.API.Endpoints.Favorite.Get
{
    /// <summary>
    /// Endpoint for retrieving a user's favorite superheroes.
    /// </summary>
    /// <remarks>
    /// This endpoint allows clients to retrieve a list of superheroes that have been marked as favorites by a user.
    /// Users can access their favorite superheroes through this endpoint.
    /// </remarks>
    public class Endpoint(IFavoriteRepository favoriteRepository, ILogger<Endpoint> logger) : Endpoint<Request, Response>
    {
        private readonly IFavoriteRepository _favoriteRepository = favoriteRepository;
        private readonly ILogger<Endpoint> _logger = logger;

        /// <inheritdoc/>
        public override void Configure()
        {
            _logger.LogDebug("Configuring endpoint: {Endpoint}", GetType().Name);
            Get("/api/favorite/get");
            AllowAnonymous();
            Summary(s => {
                s.Summary = "Retrieve User's Favorite Superheroes";
                s.Description = "This endpoint allows clients to retrieve a list of superheroes that have been marked as favorites by a user.";
            });
        }

        /// <inheritdoc/>
        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            _logger.LogDebug("Retrieving favorite superheroes for user ID: {UserId}", req.UserId);
            var favorites = await _favoriteRepository.GetAsync(req.UserId);
            await SendAsync(new() { SuperheroIds = favorites }, cancellation: ct);
        }
    }
}
