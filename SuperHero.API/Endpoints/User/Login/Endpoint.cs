using FastEndpoints;
using SuperHero.API.Services.Interfaces;
using SuperHero.API.Utils;

namespace SuperHero.API.Endpoints.User.Login
{
    /// <summary>
    /// Endpoint for user authentication (login).
    /// </summary>
    /// <remarks>
    /// This endpoint allows clients to authenticate users by providing their username and password.
    /// Upon successful authentication, a JWT token is generated and returned to the client for subsequent requests.
    /// </remarks>
    public class Endpoint(IUserService userService, IAuthUtils authUtils) : Endpoint<Request, Response>
    {
        private readonly IUserService _userService = userService;
        private readonly IAuthUtils _authUtils = authUtils;

        /// <inheritdoc/>
        public override void Configure()
        {
            Post("/api/user/login");
            AllowAnonymous();
            Summary(s => {
                s.Summary = "User Authentication (Login)";
                s.Description = "This endpoint allows clients to authenticate users by providing their username and password. Upon successful authentication, a JWT token is generated and returned to the client for subsequent requests.";
            });
        }

        /// <inheritdoc/>
        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var user = await _userService.AuthenticateAsync(req.Username, req.Password);
            if (user == null)
                ThrowError("Invalid username or password.");

            var token = _authUtils.GenerateJwtToken(user.Username);
            await SendAsync(new() { Token = token }, cancellation: ct);
        }
    }
}
