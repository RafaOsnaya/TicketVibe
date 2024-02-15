using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using TicketVibe.Entities;
using TicketVibe.Repositories;
using TicketVibe.Dto;
using System.Net;

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
            var response = new BaseResponseGeneric<ICollection<Genre>>();
            try
            {
                response.Data = await genreRepository.GetAllGenresAsync();
                response.Success = true;
                logger.LogInformation("Se Obtenieron  toda la informacion de los generos musicales");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener la informacion.";
                logger.LogError(ex, $"{ex.Message}{response.ErrorMessage}");
                return BadRequest(response);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetGenreById(int id)
        {
            var response = new BaseResponseGeneric<Genre>();
            try
            {
                response.Data = await genreRepository.GetGenreByIdAsync(id);
                response.Success = true;

                if (response.Data is null)
                {
                    logger.LogWarning($"No se encontro el genero musical {id}.");
                    return NotFound(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = "Ocurrio un error al obtener la informacion.";
                logger.LogError(ex, $"{ex.Message}{response.ErrorMessage}");
                return BadRequest(response);
            }
        }
        [HttpPost] 
        public async Task<IActionResult> AddGenre(Genre genre)
        {
            var response = new BaseResponseGeneric<int>();

            try
            {
                await genreRepository.AddAsync(genre);
                response.Data = genre.Id;
                response.Success = true;
                logger.LogInformation($"Agregando el genero {genre.Id}.");
                return StatusCode((int)HttpStatusCode.Created, response); // 201 (Created
                //return CreatedAtAction(nameof(GetGenreById), new { id = genre.Id }, genre);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al insertar la informacion.";
                logger.LogError(ex, $"{ex.Message}{response.ErrorMessage}");
                return BadRequest(response);
            }
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateGenre(int id, Genre genre)
        {
            var response = new BaseResponse();
            try
            {
                var item = await genreRepository.GetGenreByIdAsync(id);
                if (item is null)
                {
                    logger.LogInformation($"No se encontro el genero musical {id}.");
                    return NotFound(response);
                }
                await genreRepository.UpdateGenreAsync(id, genre);
                response.Success = true;
                logger.LogInformation($"Actualizando el genero {id}.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al actualizar la informacion.";
                logger.LogError(ex, $"{ex.Message}{response.ErrorMessage}");
                return BadRequest(response);
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var response = new BaseResponse();
            try
            {
                var item = await genreRepository.GetGenreByIdAsync(id);
                if (item is null)
                {
                    logger.LogInformation($"No se encontro el genero musical {id}.");
                    return NotFound(response);
                }
                await genreRepository.DeleteGenreAsync(id);
                response.Success = true;
                logger.LogInformation($"Borrando el genero {id}.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al borrar la informacion.";
                logger.LogError(ex, $"{ex.Message}{response.ErrorMessage}");
                return BadRequest(response);
            }
        }
    }
}
