namespace RussianBathHouse.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using RussianBathHouse.Data.Models;

    public class BathHouseDbContext : IdentityDbContext<ApplicationUser>
    {
        public BathHouseDbContext(DbContextOptions<BathHouseDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cabin> Cabins { get; init; }

        public DbSet<Reservation> Reservations { get; init; }

        public DbSet<Service> Services { get; init; }

        public DbSet<Accessory> Accessories { get; init; }

        public DbSet<ReservationService> ReservationServices { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Reservation>()
                .HasOne(r => r.Cabin)
                .WithMany(c => c.Reservations)
                .OnDelete(DeleteBehavior.Restrict);

           builder.Entity<ReservationService>()
               .HasKey(rs => new { rs.ServiceId, rs.ReservationId });
          
           builder.Entity<ReservationService>()
               .HasOne(rs => rs.Service)
               .WithMany(s => s.ReservationServices)
               .HasForeignKey(s => s.ServiceId)
               .OnDelete(DeleteBehavior.Restrict);
          
           builder.Entity<ReservationService>()
               .HasOne(rs => rs.Reservation)
               .WithMany(r => r.ReservationServices)
               .HasForeignKey(r => r.ReservationId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Reservation>()
                .HasOne<ApplicationUser>()
                .WithMany(r => r.Reservations)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);
          
           base.OnModelCreating(builder);
        }
    }
}
