using Microsoft.EntityFrameworkCore;
using TicketVibe.Entities;
using TicketVibe.Persistence;
namespace TicketVibe.Repositories

{
    public class GenreRepository : IGenreRepository
    {
        //Dependency Injection
        private readonly ApplicationDBContext context;


        //Constructor
        public GenreRepository(ApplicationDBContext context)
        {
            this.context = context;

        }

        //Metodos
        public async Task<List<Genre>> GetAllGenresAsync()
        {
            return await context.Genres
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Genre?> GetGenreByIdAsync(int id)
        {
            
            var item = await context.Genres
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item is not null)
            {
                return item;
            }
            else
            {
                throw new InvalidOperationException($"Genre not found id {id}");
            }
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
                throw new InvalidOperationException($"Genre not found id {id}");
            }
        }

        public async Task DeleteGenreAsync(int id)
        {
            var item = await GetGenreByIdAsync(id);
            if (item is not null)
            {
                context.Genres.Remove(item);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"Genre not found id {id}");
            }
        }
    }
}
