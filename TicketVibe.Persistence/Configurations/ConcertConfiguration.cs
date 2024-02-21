using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketVibe.Entities;


namespace TicketVibe.Persistence.Configurations
{
    internal class ConcertConfiguration : IEntityTypeConfiguration<Concert>
    {
        public void Configure(EntityTypeBuilder<Concert> builder)
        {
            builder.Property(c => c.Title).HasMaxLength(100);
            builder.Property(c => c.Description).HasMaxLength(200);
            builder.Property(c => c.Place).HasMaxLength(100);
            builder.Property(c => c.UnitPrice).HasColumnType("decimal(10,2)");
            builder.Property(c => c.DateEvent)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");
            builder.Property(c => c.ImageUrl)
                .HasMaxLength(2000)
                .IsUnicode(false);
            

            builder.HasIndex(c => c.Title).IsUnique();
            builder.ToTable("Concert", schema: "Musicales");
            

        }
    }
}
