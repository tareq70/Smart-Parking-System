using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Smart_Parking_System.Domain_Layer.Entities;
using Smart_Parking_System.DomainLayer.Entities;

namespace Smart_Parking_System.Infrastructure.Data
{
    public class AppDbcontext : IdentityDbContext<ApplicationUser>
    {
        public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options)
        {

        }
        public DbSet<ParkingArea> ParkingAreas { get; set; }
        public DbSet<ParkingSpot> ParkingSpots { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParkingArea>().HasKey(p => p.Id);
            modelBuilder.Entity<ParkingArea>().Property(p => p.Name).IsRequired().HasMaxLength(100);
            
            modelBuilder.Entity<ParkingArea>()
            .HasMany(a => a.Spots)
            .WithOne(s => s.ParkingArea)
            .HasForeignKey(s => s.ParkingAreaId);

        }



    }
}
