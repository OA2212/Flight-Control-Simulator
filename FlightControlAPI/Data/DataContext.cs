using CommonModels;
using Microsoft.EntityFrameworkCore;

namespace FlightControlAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Plane> Planes { get; set; }
        public virtual DbSet<AirportLocation> Locations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>()
            .HasOne(f => f.Plane)
            .WithOne(p => p.Flight)
            .HasForeignKey<Plane>(p => p.FlightId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Plane>()
                .HasOne(p => p.AirportLocation)
                .WithMany(l => l.Planes)
                .HasForeignKey(p => p.AirportLocationId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
