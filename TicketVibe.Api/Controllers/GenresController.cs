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


    }
}
