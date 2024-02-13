using TicketVibe.Entities;

namespace TicketVibe.Repositories
{
    public interface IGenreRepository
    {
        Task<int> AddAsync(Genre genre);
        Task DeleteGenreAsync(int id);
        Task<List<Genre>> GetAllGenresAsync();
        Task<Genre?> GetGenreByIdAsync(int id);
        Task UpdateGenreAsync(int id, Genre genre);
    }
}