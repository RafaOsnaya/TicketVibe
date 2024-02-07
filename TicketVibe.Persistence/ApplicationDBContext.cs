using Microsoft.EntityFrameworkCore;
using TicketVibe.Entities;

namespace TicketVibe.Persistence
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {

        }

        //Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<Genre>().Property(g => g.Name).IsRequired().HasMaxLength(50);        

        }

        //Entities To Tables
        public DbSet<Genre> Genres { get; set; }


    }
}
