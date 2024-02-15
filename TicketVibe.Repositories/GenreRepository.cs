using Microsoft.EntityFrameworkCore;
using TicketVibe.Dto.Request;
using TicketVibe.Dto.Response;
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
        public async Task<List<GenreResponseDto>> GetAllGenresAsync()
        {
            var items = await context.Genres
                .AsNoTracking()
                .ToListAsync();

            //Mapping
            var genresResponseDto = items.Select(x => new GenreResponseDto
            {
                Id = x.Id,
                Name = x.Name,
                Status = x.Status
            }).ToList();

            return genresResponseDto;
        }

        public async Task<GenreResponseDto?> GetGenreByIdAsync(int id)
        {
            
            var item = await context.Genres
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            var genreResponseDto = new GenreResponseDto();

            if (item is not null)
            {
                //Mapping    
                genreResponseDto.Id = item.Id;
                genreResponseDto.Name = item.Name;
                genreResponseDto.Status = item.Status;
                
            }
            else
            {
                throw new InvalidOperationException($"Genre not found id {id}");
               
            }
            return genreResponseDto;
        }

        public async Task<int> AddAsync(GenreRequestDto genreRequestDto)
        {
            //MApping
            var genre = new Genre
            {
                Name = genreRequestDto.Name,
                Status = genreRequestDto.Status
            };

            context.Genres.Add(genre);
            await context.SaveChangesAsync();
            return genre.Id;
        }

        public async Task UpdateGenreAsync(int id, GenreRequestDto genreRequestDto)
        {
            var item = await context.Genres
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item is not null)
            {
                //Mapping

                item.Name = genreRequestDto.Name;
                item.Status = genreRequestDto.Status;
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
            var item = await context.Genres
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

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
