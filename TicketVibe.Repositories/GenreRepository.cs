using Microsoft.EntityFrameworkCore;
using TicketVibe.Entities;
using TicketVibe.Persistence;
namespace TicketVibe.Repositories

{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDBContext context;

        public GenreRepository(ApplicationDBContext context)
        {
            this.context = context;

        }

        //Metodos
        public async Task<List<Genre>> GetAllGenresAsync()
        {
            return await context.Genres.ToListAsync();
        }

        public async Task<Genre?> GetGenreByIdAsync(int id)
        {
            return await context.Genres.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> AddAsync(Genre genre)
        {
            context.Genres.Add(genre);
            await context.SaveChangesAsync();
            return genre.Id;
        }

        public async Task UpdateGenreAsync(int id, Genre genre)
        {
            var item = await GetGenreByIdAsync(id);
            if (item is not null)
            {
                item.Name = genre.Name;
                item.Status = genre.Status;
                context.Update(item);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Genre not found");
            }
        }

        public async Task DeleteGenreAsync(int id)
        {
            var item = await GetGenreByIdAsync(id);
            if (item is not null)
            {
                context.Remove(item);
            }
            else
            {
                throw new Exception("Genre not found");
            }
        }
    }
}
