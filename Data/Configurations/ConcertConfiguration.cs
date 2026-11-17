using ConcertTicketBooking.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConcertTicketBooking.Data.Configurations
{
    public class ConcertConfiguration
        : IEntityTypeConfiguration<Concert>
    {
        public void Configure(EntityTypeBuilder<Concert> builder)
        {
            // Database table name
            builder.ToTable("Concerts");

            // Primary key
            builder.HasKey(c => c.ConcertId);

            // Concert name
            builder.Property(c => c.ConcertName)
                .IsRequired()
                .HasMaxLength(150);

            // Artist name
            builder.Property(c => c.ArtistName)
                .IsRequired()
                .HasMaxLength(150);

            // Description
            builder.Property(c => c.Description)
                .HasMaxLength(1000);

            // Genre
            builder.Property(c => c.Genre)
                .IsRequired()
                .HasMaxLength(80);

            // Language
            builder.Property(c => c.Language)
                .IsRequired()
                .HasMaxLength(50);

            // Image URL
            builder.Property(c => c.ImageUrl)
                .HasMaxLength(500);

            // Store enum as readable string
            builder.Property(c => c.Status)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            // Audit properties
            builder.Property(c => c.CreatedAtUtc)
                .IsRequired();

            // Index for searching concert names
            builder.HasIndex(c => c.ConcertName);
        }
    }
}