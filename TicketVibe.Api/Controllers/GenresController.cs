using Microsoft.AspNetCore.Mvc;
using TicketVibe.Entities;
using TicketVibe.Repositories;

namespace TicketVibe.Api.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreRepository genreRepository;

        public GenresController(IGenreRepository genreRepository)
        {
            this.genreRepository = genreRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            var data = await genreRepository.GetAllGenresAsync();
            return Ok(data);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetGenreById(int id)
        {
            var item = await genreRepository.GetGenreByIdAsync(id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        [HttpPost] 
        public async Task<IActionResult> AddGenre(Genre genre)
        {
            await genreRepository.AddAsync(genre);
            return Ok(genre);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateGenre(int id, Genre genre)
        {
            await genreRepository.UpdateGenreAsync(id, genre);
            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            await genreRepository.DeleteGenreAsync(id);
            return NoContent();
        }

    }
}
