namespace SuperHero.API.Data.Interfaces
{
    public interface IFavoriteRepository
    {
        Task AddAsync(int superHeroId, int userId);
        Task<List<int>> GetAsync(int userId);
    }
}
