using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SuperHero.API.ServiceRegistration
{
    public static class AuthRegistration
    {
        public static IServiceCollection RegisterAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var secretKey = configuration["SecretKey"];
            if (string.IsNullOrEmpty(secretKey))
                throw new Exception("SecretKey is missing in appsettings.json");
            
            services.AddAuthentication(options =>
             {
                 options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                 options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
             })
            .AddCookie()
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
            return services;
        }
    }
}
