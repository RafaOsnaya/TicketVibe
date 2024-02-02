using Microsoft.AspNetCore.Mvc;
using TicketVibe.Entities;
using TicketVibe.Repositories;

namespace TicketVibe.Api.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenresController : ControllerBase
    {
        private readonly GenreRepository genreRepository;

        public GenresController(GenreRepository genreRepository)
        {
            this.genreRepository = genreRepository;
        }

        [HttpGet]
        public ActionResult<List<Genre>> GetAllGenres()
        {
            return Ok(genreRepository.GetAllGenres());
        }
        [HttpGet("{id:int}")]
        public ActionResult<Genre?> GetGenreById(int id)
        {
            var genre = genreRepository.GetGenreById(id);
            if (genre is null)
            {
                return NotFound();
            }
            return Ok(genre);
        }
        [HttpPost] 
        public ActionResult<Genre> AddGenre(Genre genre)
        {
            genreRepository.AddGenre(genre);
            return Ok(genre);
        }
        [HttpPut("{id:int}")]
        public ActionResult<Genre> UpdateGenre(int id, Genre genre)
        {
            var existingGenre = genreRepository.GetGenreById(id);
            if (existingGenre is null)
            {
                return NotFound();
            }
            genreRepository.UpdateGenre(id, genre);
            return Ok(genre);
        }
        [HttpDelete("{id:int}")]
        public ActionResult DeleteGenre(int id)
        {
            var existingGenre = genreRepository.GetGenreById(id);
            if (existingGenre is null)
            {
                return NotFound();
            }
            genreRepository.DeleteGenre(id);
            return Ok();
        }

    }
}
