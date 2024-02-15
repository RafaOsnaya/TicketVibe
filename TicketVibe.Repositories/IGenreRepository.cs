using TicketVibe.Dto.Request;
using TicketVibe.Dto.Response;
using TicketVibe.Entities;

namespace TicketVibe.Repositories
{
    public interface IGenreRepository
    {
        Task<int> AddAsync(GenreRequestDto genre);
        Task DeleteGenreAsync(int id);
        Task<List<GenreResponseDto>> GetAllGenresAsync();
        Task<GenreResponseDto?> GetGenreByIdAsync(int id);
        Task UpdateGenreAsync(int id, GenreRequestDto genre);
    }
}