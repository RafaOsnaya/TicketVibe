using Microsoft.EntityFrameworkCore;
using TicketVibe.Dto.Request;
using TicketVibe.Dto.Response;
using TicketVibe.Entities;
using TicketVibe.Persistence;
namespace TicketVibe.Repositories

{
    public class GenreRepository : RepositoryBase<Genre>, IGenreRepository
    {

        //Constructor
        public GenreRepository(ApplicationDBContext context) : base(context) 
        { 
            
        }        
    }
}
