using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using TicketVibe.Entities;
using TicketVibe.Repositories;

namespace TicketVibe.Api.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreRepository genreRepository;
        private readonly ILogger<GenresController> logger;

        public GenresController(IGenreRepository genreRepository, ILogger<GenresController> logger)
        {
            this.genreRepository = genreRepository;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            try
            {
                var data = await genreRepository.GetAllGenresAsync();
                logger.LogInformation("Se Obtenieron  toda la informacion de los generos musicales");
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex.Message}", "Error al obtener la informacion");
                return BadRequest("Ocurrio un error al obtener la informacion.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetGenreById(int id)
        {
            try
            {
                var item = await genreRepository.GetGenreByIdAsync(id);
                logger.LogInformation($"No se Obtuvo la informacion del genero {id}.");

                if (item is null)
                {
                    logger.LogWarning($"No se encontro el genero musical {id}.");
                    return NotFound("No se encontro el registro");
                }
                return Ok(item);
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex.Message}", "Error al obtener la informacion");
                return BadRequest("Ocurrio un error al obtener la informacion.");
            }
        }
        [HttpPost] 
        public async Task<IActionResult> AddGenre(Genre genre)
        {
            try
            {
                await genreRepository.AddAsync(genre);
                logger.LogInformation($"Agregando el genero {genre.Id}.");
                return CreatedAtAction(nameof(GetGenreById), new { id = genre.Id }, genre);
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex.Message}", "Error al insertar la informacion");
                return BadRequest("Ocurrio un error al insertar la informacion.");
            }
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateGenre(int id, Genre genre)
        {
            try
            {
                var item = await genreRepository.GetGenreByIdAsync(id);
                if (item is null)
                {
                    logger.LogInformation($"No se encontro el genero musical {id}.");
                    return NotFound("No se encontro el registro");
                }
                await genreRepository.UpdateGenreAsync(id, genre);
                logger.LogInformation($"Actualizando el genero {id}.");
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex.Message}", "Error al actualizar la informacion");
                return BadRequest("Ocurrio un error al actualizar la informacion.");
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            try
            {
                var item = await genreRepository.GetGenreByIdAsync(id);
                if (item is null)
                {
                    logger.LogInformation($"No se encontro el genero musical {id}.");
                    return NotFound("No se encontro el registro");
                }
                await genreRepository.DeleteGenreAsync(id);
                logger.LogInformation($"Borrando el genero {id}.");
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex.Message}", "Error al borrar la informacion");
                return BadRequest("Ocurrio un error al eliminar la informacion.");
            }
        }
    }
}
