using Microsoft.EntityFrameworkCore;
using System.Reflection;
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

            //Apply All Configurations (Fluent API) (Reflection Librar
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); 
                 

        }


    }
}
