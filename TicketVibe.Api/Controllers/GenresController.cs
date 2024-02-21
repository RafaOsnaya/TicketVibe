using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using TicketVibe.Entities;
using TicketVibe.Repositories;
using TicketVibe.Dto;
using System.Net;
using TicketVibe.Dto.Response;
using TicketVibe.Dto.Request;

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
            var response = new BaseResponseGeneric<ICollection<GenreResponseDto>>();
            try
            {
                //Mapping
                var genreDb = await genreRepository.GetAllAsync();
                var genresResposeDto = genreDb.Select(x => new GenreResponseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Status = x.Status
                }).ToList();


                response.Data = genresResposeDto;
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
            var response = new BaseResponseGeneric<GenreResponseDto>();
            try
            {

                var genresDb = await genreRepository.GetByIdAsync(id);
                if (genresDb is null)
                {
                    logger.LogWarning($"No se encontro el genero musical {id}.");
                    return NotFound(response);
                }
                else
                {
                    var genreResposeDto = new GenreResponseDto()
                    {
                        Id = genresDb.Id,
                        Name = genresDb.Name,
                        Status = genresDb.Status
                    };
                    response.Data = genreResposeDto;
                    response.Success= true;
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
        public async Task<IActionResult> AddGenre(GenreRequestDto genreRequestDto)
        {
            var response = new BaseResponseGeneric<int>();

            try
            {
                var genresDb = new Genre()
                {
                    Name = genreRequestDto.Name,
                    Status = genreRequestDto.Status
                };


                var genreId = await genreRepository.AddAsync(genresDb);
                response.Data = genreId;
                response.Success = true;
                logger.LogInformation($"Agregando el genero {genreId}.");
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
        public async Task<IActionResult> UpdateGenre(int id, GenreRequestDto genreRequestDto)
        {
            var response = new BaseResponse();
            try
            {
                var genresDb = await genreRepository.GetByIdAsync(id);

                if (genresDb is null) {

                    response.ErrorMessage = "No se encontro el registro.";
                    return NotFound(response);
                }
                genresDb.Name = genreRequestDto.Name;
                genresDb.Status = genreRequestDto.Status;

                await genreRepository.UpdateAsync();
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
                var genresDb = await genreRepository.GetByIdAsync(id);

                if (genresDb is null)
                {

                    response.ErrorMessage = "No se encontro el registro.";
                    return NotFound(response);
                }


                await genreRepository.DeleteAsync(id);
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
