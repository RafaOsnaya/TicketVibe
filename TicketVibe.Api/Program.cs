using Microsoft.EntityFrameworkCore;
using TicketVibe.Entities;
using TicketVibe.Persistence;
using TicketVibe.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configuring context
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});



//Registering  services
builder.Services.AddTransient<IGenreRepository, GenreRepository>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

/*app.MapGet("api/holaMundo", () => "Hello World!");
app.MapGet("api/genres-minimal", (GenreRepository genreRepository) =>
{
    return genreRepository.GetAllGenres();
});

app.MapPost("api/genres-minimal", (Genre genre, GenreRepository genreRepository) =>
{
    genreRepository.AddGenre(genre);
    return Results.Ok(genre);
});
*/
app.MapControllers();

app.Run();
