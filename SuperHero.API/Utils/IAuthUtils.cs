namespace SuperHero.API.Utils
{
    public interface IAuthUtils
    {
        string GenerateJwtToken(string username);
        byte[] HashPassword(string password);
    }
}
