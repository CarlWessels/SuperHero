using FastEndpoints;
using SuperHero.API.Services.Interfaces;
using SuperHero.API.Utils;

namespace SuperHero.API.Endpoints.User.Register
{
    /// <summary>
    /// Endpoint for user registration.
    /// </summary>
    /// <remarks>
    /// This endpoint allows clients to register new users by providing a username and password.
    /// Upon successful registration, a JWT token is generated and returned to the client for subsequent requests.
    /// </remarks>
    public class Endpoint(IUserService userService, IAuthUtils authUtils) : Endpoint<Request, Response>
    {
        private readonly IUserService _userService = userService;
        private readonly IAuthUtils _authUtils = authUtils;

        /// <inheritdoc/>
        public override void Configure()
        {
            Post("/api/user/register");
            AllowAnonymous();
            Summary(s => {
                s.Summary = "User Registration";
                s.Description = "This endpoint allows clients to register new users by providing a username and password. Upon successful registration, a JWT token is generated and returned to the client for subsequent requests.";
            });
        }

        /// <inheritdoc/>
        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var userId = await _userService.RegisterAsync(req.User.Username, req.User.Password);
            if (userId == null)
                ThrowError("Failed to register user.");

            var token = _authUtils.GenerateJwtToken(req.User.Username);
            await SendAsync(new() { Token = token }, cancellation: ct);
        }
    }
}
