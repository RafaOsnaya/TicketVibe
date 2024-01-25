using TicketVibe.Entities;
namespace TicketVibe.Repositories

{
    public class GenreRepository
    {
        private readonly List<Genre> genreList = new List<Genre>();

        public GenreRepository()
        {
            genreList.Add(new Genre { Id = 1, Name = "Hip Hop"});
            genreList.Add(new Genre { Id = 2, Name = "R&B"});
            genreList.Add(new Genre { Id = 3, Name = "Salsa" });
            genreList.Add(new Genre { Id = 4, Name = "Cumbia"});
            genreList.Add(new Genre { Id = 5, Name = "Rock" });
        }

        //Metodos
        public List<Genre> GetAllGenres()
        {
            return genreList;
        }

        public Genre? GetGenreById(int id)
        {
            return genreList.FirstOrDefault(x => x.Id == id);
        }

        public void AddGenre(Genre genre)
        {
            genre.Id = genreList.Max(x => x.Id) + 1;
            genreList.Add(genre);
        }

        public void UpdateGenre(int id,  Genre genre)
        {
            var item = GetGenreById(id);
            if(item is not null)
            {
                item.Name = genre.Name;
                item.Status = genre.Status;
            }
            else
            {
                throw new Exception("Genre not found");
            }
        }

        public void DeleteGenre(int id)
        {
            var item = GetGenreById(id);
            if (item is not null)
            {
                genreList.Remove(item);
            }
            else
            {
                throw new Exception("Genre not found");
            }
        }

    }
}
