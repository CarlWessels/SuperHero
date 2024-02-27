using FastEndpoints;
using SuperHero.API.Data.Interfaces;

namespace SuperHero.API.Endpoints.Favorite.Add
{
    /// <summary>
    /// Endpoint for adding a superhero to a user's favorites list.
    /// </summary>
    /// <remarks>
    /// This endpoint allows clients to add a superhero to a user's list of favorite superheroes.
    /// Users can mark superheroes as favorites to easily access them later.
    /// </remarks>
    public class Endpoint(IFavoriteRepository favoriteRepository, ILogger<Endpoint> logger) : Endpoint<Request, Response>
    {
        private readonly IFavoriteRepository _favoriteRepository = favoriteRepository;
        private readonly ILogger<Endpoint> _logger = logger;

        /// <inheritdoc/>
        public override void Configure()
        {
            _logger.LogDebug("Configuring endpoint: {Endpoint}", GetType().Name);
            Post("/api/favorite/add");
            AllowAnonymous();
            Summary(s => {
                s.Summary = "Add a Superhero to Favorites";
                s.Description = "This endpoint allows clients to add a superhero to their list of favorite superheroes.";
            });
        }

        /// <inheritdoc/>
        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            _logger.LogDebug("Adding superhero ID: {SuperheroId} to user ID: {UserId}'s favorites list", req.SuperheroId, req.UserId);
            await _favoriteRepository.AddAsync(req.SuperheroId, req.UserId);
            await SendAsync(new(), cancellation: ct);
        }
    }
}
