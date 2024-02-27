using SuperHero.API.Models;

namespace SuperHero.API.Data.Interfaces
{
    public interface ISuperheroRepository
    {
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Superhero>> GetAllAsync();
        Task<Superhero> GetByIdAsync(int id);
        Task<int> InsertAsync(Superhero superhero);
        Task<bool> UpdateAsync(Superhero superhero);
        Task<IEnumerable<Superhero>> SearchSuperHeroAsync(string? search);
    }
}
